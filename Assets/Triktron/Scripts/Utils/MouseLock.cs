using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    public bool LockOnStart;

    void Start()
    {
        current = this;
#if !UNITY_EDITOR
        if (LockOnStart)
            Lock();
        else
            Unlock();
#endif
    }


    public static MouseLock current;

    public void Lock()
    {
        if (!PauseMenu.Paused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Unlock()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
