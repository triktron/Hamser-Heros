using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    public Transform Object;
    ParticleSystem.MainModule ps;
    public Vector3 Offset;
    void Start()
    {
        ps = GetComponent<ParticleSystem>().main;
    }

    // Update is called once per frame
    void Update()
    {
        ps.startRotationZ = -(Object.rotation.eulerAngles.y) * Mathf.Deg2Rad;
        transform.position = Object.position - Offset;
    }
}
