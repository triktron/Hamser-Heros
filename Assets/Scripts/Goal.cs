using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public MonoBehaviour[] ScriptToEnable;
    public MonoBehaviour[] ScriptToDisable;
    public Rigidbody[] BodiesToFreeze;

    private void OnTriggerEnter(Collider other)
    {
        foreach (MonoBehaviour component in ScriptToEnable)
        {
            component.enabled = true;
        }
        foreach (MonoBehaviour component in ScriptToDisable)
        {
            component.enabled = false;
        }
        foreach (Rigidbody component in BodiesToFreeze)
        {
            component.isKinematic = true;
        }
        enabled = false;
    }
}
