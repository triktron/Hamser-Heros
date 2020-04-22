using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
#if (!UNITY_EDITOR)
    void Start()
    {
        Lock();
    }
#endif

    public void Lock()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Unlock()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
