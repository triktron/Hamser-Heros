using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePositionAndHeading : MonoBehaviour
{
    public Transform ObjectToFollow;
    public Vector3 Offset;
    public float MaxHeadingDistance = 0.1f;
    public float lookSpeed = 10;

    Vector3 LastHeading;
    Vector3 _lookDirection;

    // Update is called once per frame
    void FixedUpdate()
    {
         transform.position = ObjectToFollow.position - Offset;

        if (Vector3.Distance(transform.position, LastHeading) > MaxHeadingDistance)
        {
            _lookDirection = transform.position - LastHeading;

            LastHeading = transform.position;
        }

        Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y,0);
    }
}
