using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationCamera : MonoBehaviour
{
    public Vector3 Offset;
    Transform Target;
    public float Speed;

    public float MaxDeg;
    float CurrentDeg;
    public float OffsetDeg;

    public void Awaken(Transform target)
    {
        Target = target;
        enabled = true;
        CurrentDeg = OffsetDeg;
        Debug.Log("Starting Animation");

        if (Timer.GetTime() != 0) Timer.StopTimer();
    }
    public void Sleep(Transform target)
    {
        enabled = false;
    }

    void Start()
    {
        CurrentDeg = OffsetDeg;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentDeg += Time.deltaTime * Speed;
        if (CurrentDeg >= MaxDeg) CurrentDeg = MaxDeg;

        transform.position = Target.position + Quaternion.Euler(0, CurrentDeg, 0) * Offset;
        transform.LookAt(Target);

        if (CurrentDeg >= MaxDeg)
        {
            Manager.main.StartNextState();
            enabled = false;
        }
    }
}
