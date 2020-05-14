using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class VRController : MonoBehaviour
{
    [System.Serializable]
    public class UnityEventVector2 : UnityEvent<Vector2>
    {
    }

    public SteamVR_Action_Vector2 touchPad;
    public UnityEventVector2 Move;
    // Update is called once per frame
    void Update()
    {
        Move.Invoke(touchPad.GetAxis(SteamVR_Input_Sources.Any));
    }
}
