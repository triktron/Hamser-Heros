using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 100;

    Vector3 Movement;
    Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float angle = Camera.main.transform.rotation.eulerAngles.y;

        Vector3 forward = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle), 0, Mathf.Cos(Mathf.Deg2Rad * angle));
        Vector3 left = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, -Mathf.Sin(Mathf.Deg2Rad * angle));


        Movement = Input.GetAxis("Horizontal") * left + Input.GetAxis("Vertical") * forward;

        Debug.DrawRay(transform.position, forward * 2, Color.red);
        Debug.DrawRay(transform.position, left * 2, Color.green);
        Debug.DrawRay(transform.position, Movement * 2, Color.magenta);
    }

    void FixedUpdate()
    {
        rb.AddForce(Movement * Speed);
    }
}
