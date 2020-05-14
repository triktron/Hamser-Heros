using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    public float TriggerSpeed = 5;

    Rigidbody RB;

    AudioSource AS;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
        RB = GetComponent<Rigidbody>();
    }

    float LastSpeed;

    // Works with regular fields
    [DebugGUIGraph(r: 0, g: 1, b: 0, autoScale: true)]
    float VelocetyChange;
    private void FixedUpdate()
    {
        float currentSpeed = RB.velocity.sqrMagnitude;
        VelocetyChange = Mathf.Abs(LastSpeed - currentSpeed);
        if (AS != null && VelocetyChange > TriggerSpeed) AS.Play();
        LastSpeed = currentSpeed;
    }
}
