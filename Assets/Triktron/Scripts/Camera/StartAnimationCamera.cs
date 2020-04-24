using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationCamera : MonoBehaviour
{
    public Vector3 Offset;
    public Transform Player;
    public float Speed;

    public float MaxDeg;
    float CurrentDeg;
    public float OffsetDeg;

    public MonoBehaviour[] ScriptToEnable;
    public MonoBehaviour[] ScriptToDisable;

    void Start()
    {
        CurrentDeg = OffsetDeg;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentDeg += Time.deltaTime * Speed;
        if (CurrentDeg >= MaxDeg) CurrentDeg = MaxDeg;

        transform.position = Player.position + Quaternion.Euler(0, CurrentDeg, 0) * Offset;
        transform.LookAt(Player);

        if (CurrentDeg >= MaxDeg)
        {
            foreach (MonoBehaviour component in ScriptToEnable)
            {
                component.enabled = true;
            }
            foreach (MonoBehaviour component in ScriptToDisable)
            {
                component.enabled = false;
            }
            enabled = false;
        }
    }
}
