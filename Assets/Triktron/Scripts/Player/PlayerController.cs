using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float Speed = 100;

    public float GroundScanLength;

    Vector3 Movement;
    Rigidbody rb;

    ParticleSystem Paws;

    bool IsOnGround;

    public CameraFollowController Cam;
    public Transform CamTransform;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Paws = FindObjectOfType<ParticleSystem>();

        Manager.main.StartAnimation.AddListener(FreezeBody);
        Manager.main.Playing.AddListener(UnFreezeBody);
        Manager.main.PlayerMove.AddListener(Move);
    }

    private void UnFreezeBody(Transform arg0)
    {
        rb.isKinematic = false;
        Timer.StartTimer();
    }

    private void FreezeBody(Transform arg0)
    {
        rb.isKinematic = true;
    }


    void Update()
    {
        if (CamTransform == null && Cam != null) CamTransform = Cam.transform;
        if (CamTransform != null)
        {
            float angle = CamTransform.rotation.eulerAngles.y;

            Vector3 forward = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
            Vector3 left = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, -Mathf.Sin(Mathf.Deg2Rad * angle));

            Debug.DrawRay(transform.position, forward * 2, Color.red);
            Debug.DrawRay(transform.position, left * 2, Color.green);
            Debug.DrawRay(transform.position, Movement * 2, Color.magenta);
            Debug.DrawRay(transform.position, Vector3.down * GroundScanLength);
        }

        if (!Paws.isPlaying && IsOnGround) Paws.Play();
        if (Paws.isPlaying && !IsOnGround) Paws.Stop();
    }
    public void Move(Vector2 input)
    {
        if (CamTransform == null && Cam != null) CamTransform = Cam.transform;
        Debug.Log("hitting");
        if (CamTransform != null)
        {
            float angle = CamTransform.rotation.eulerAngles.y;

            Vector3 forward = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
            Vector3 left = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, -Mathf.Sin(Mathf.Deg2Rad * angle));

            Movement = input.x * left + input.y * forward;
        }
    }

    bool IsOnLayerMask(int layer, LayerMask mask)
    {
        return mask == (mask | (1 << layer));
    }


    void FixedUpdate()
    {
        RaycastHit info;
        IsOnGround = Physics.Raycast(transform.position, Vector3.down, out info, GroundScanLength);

        if (IsOnGround) rb.AddForce(Movement * Speed);
    }
}
