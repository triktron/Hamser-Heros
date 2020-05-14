using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float Speed = 5;

    CharacterController cc;
    ParticleSystem Paws;

    Vector3 Input;

    Vector3[] Forces;
    Vector3 Veclocity;
    public Vector3 Gravety = new Vector3(0,-5,0);

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Paws = FindObjectOfType<ParticleSystem>();

        Manager.main.StartAnimation.AddListener(FreezeBody);
        Manager.main.Playing.AddListener(UnFreezeBody);
        Manager.main.PlayerMove.AddListener(Move);
    }

    bool frozen = false;

    private void UnFreezeBody(Transform arg0)
    {
        frozen = false;
        Timer.StartTimer();
    }

    private void FreezeBody(Transform arg0)
    {
        frozen = true;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Camera.main.transform.rotation.eulerAngles.y;

        Vector3 forward = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
        Vector3 left = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, -Mathf.Sin(Mathf.Deg2Rad * angle));

        Vector3 PlayerInputForce = Input.x * left + Input.y * forward;

        Debug.DrawRay(transform.position + transform.up * 0.5f, PlayerInputForce, Color.red);

        Veclocity = Gravety;
        Debug.DrawRay(transform.position + transform.up * 0.5f, Gravety, Color.red);
        //Velocity += PlayerInputForce;

        Vector3 normal = Vector3.up;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1f))
        {
            normal = hit.normal;
            if (!Paws.isPlaying) Paws.Play();
        } else
        {
            if (Paws.isPlaying) Paws.Stop();
        }
        

        Vector3 c = Vector3.Cross(Vector3.up, normal);
        Vector3 u = Vector3.Cross(c, normal);
        normal = u * 0.8f;

        Veclocity += normal;
        Debug.DrawRay(transform.position + transform.up * 0.5f, normal, Color.red);
        Debug.DrawRay(transform.position + transform.up * 0.5f, Veclocity, Color.green);

        if (!frozen)
        {
            cc.Move(Veclocity * Speed * Time.deltaTime);
            cc.Move(PlayerInputForce * Speed * Time.deltaTime);
        }
    }

    public void Move(Vector2 input)
    {
        Input = input;
    }
}
