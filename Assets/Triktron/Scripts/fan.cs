using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fan : MonoBehaviour
{
    Rigidbody AffectedPlayer;
    public float AirSpeed = 5;

    void Update()
    {
        if (AffectedPlayer != null) AffectedPlayer.AddForce(transform.right * AirSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        Rigidbody otherRB = other.GetComponent<Rigidbody>();
        if (otherRB != null) AffectedPlayer = otherRB;
    }

    //When the Primitive exits the collision, it will change Color
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        Rigidbody otherRB = other.GetComponent<Rigidbody>();
        if (otherRB == AffectedPlayer) AffectedPlayer = null;
    }
}
