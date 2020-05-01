using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    static float StartTime;
    static float StopTime;

    static bool Running;

    Text TextEllement;
    static bool ShouldBlink;

    public int BlinkTimes;
    public float BlinkDuration;

    static public bool Paused;
    static float PausedSince;

    static public void ResetTimer()
    {
        UnPause();
        StopTimer(false);
        StartTime = 0;
        StopTime = 0;
    }


    static public bool IsRunning()
    {
        return Running;
    }

    static public float GetTime()
    {
        if (Paused) return PausedSince - StartTime;
        if (Running) StopTime = Time.realtimeSinceStartup;
        return StopTime - StartTime;
    }

    static public void StartTimer()
    {
        if (!Running)
        {
            Running = true;
            StartTime = Time.realtimeSinceStartup;
        }
    }

    static public void StopTimer(bool shouldBlink = true)
    {
        if (Running)
        {
            Running = false;
            StopTime = Time.realtimeSinceStartup;
            ShouldBlink = shouldBlink;
        }
    }

    static public void Pause()
    {
        if (!Paused)
        {
            Paused = true;
            PausedSince = Time.realtimeSinceStartup;
        }
    }

    static public void UnPause()
    {
        if (Paused)
        {
            Paused = false;
            StartTime += Time.realtimeSinceStartup - PausedSince;
        }
    }

    void Start()
    {
        TextEllement = GetComponent<Text>();

        StartTime = 0;
        StopTime = 0;
        Running = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TextEllement.text = GetTime().ToString("F3");
        if (ShouldBlink)
        {
            StartCoroutine(Blink());
            ShouldBlink = false;
        }
    }

    IEnumerator Blink()
    {
        for (int i = 0; i < BlinkTimes; i++)
        {
            TextEllement.CrossFadeAlpha(0, BlinkDuration, true);
            yield return new WaitForSeconds(BlinkDuration);
            TextEllement.CrossFadeAlpha(1, BlinkDuration, true);
            yield return new WaitForSeconds(BlinkDuration);
        }
    }
}
