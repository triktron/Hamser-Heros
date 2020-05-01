using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    Transform Object;
    ParticleSystem.MainModule ps;
    public Vector3 Offset;
    void Start()
    {
        ps = GetComponent<ParticleSystem>().main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.main.Player != null)
        {
            ps.startRotationZ = -(Manager.main.Player.transform.rotation.eulerAngles.y) * Mathf.Deg2Rad;
            transform.position = Manager.main.Player.transform.position - Offset;
        }
    }
}
