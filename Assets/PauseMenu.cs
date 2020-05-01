using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused;

    private void Start()
    {
        
    }

    public void openMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void Toggle()
    {
        if (enabled)
        {
            if (Paused) Close(); else Open();
        }
    }

    public void Open()
    {
        Paused = true;
        MouseLock.current.Unlock();
        Debug.Log("pausing");
        Timer.Pause();
        GetComponent<RectTransform>().localPosition = new Vector3(0, -35.2f);
        Time.timeScale = 0;
    }

    public void Close()
    {
#if !UNITY_EDITOR
        MouseLock.current.Lock();
#endif
        Paused = false;
        Timer.UnPause();
        GetComponent<RectTransform>().localPosition = new Vector3(9999, 9999f);
        Time.timeScale = 1;
    }
}
