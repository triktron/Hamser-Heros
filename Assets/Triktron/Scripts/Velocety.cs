using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocety : MonoBehaviour
{
    Rigidbody rb;
    CharacterController cc;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }


    public float Speed
    {
        get
        {
            if (rb != null) return rb.velocity.sqrMagnitude;
            if (cc != null) return cc.velocity.sqrMagnitude;

            return 0;
        }
    }
}
