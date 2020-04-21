using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sway : MonoBehaviour
{
    public Vector3 Direction;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.angularVelocity = Mathf.Sin(Time.realtimeSinceStartup) * Direction;

        rb.AddTorque(Mathf.Sin(Time.realtimeSinceStartup) * Direction);
    }
}
