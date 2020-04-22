using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;    

public class Goal : MonoBehaviour
{
    public MonoBehaviour[] ScriptToEnable;
    public MonoBehaviour[] ScriptToDisable;
    public Rigidbody[] BodiesToFreeze;

    private void OnTriggerEnter(Collider other)
    {
        Timer.StopTimer();
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

        Analytics.CustomEvent("level_one_timer", new Dictionary<string, object>
        {
            { "time_elapsed", Timer.GetTime() }
        });
        Analytics.FlushEvents();

        if (GameStateManager.IsTimeBetter(Timer.GetTime()))
        {
            GameStateManager.BestTime = Timer.GetTime();
            Highscores.AddNewHighscore(GameStateManager.Username, Timer.GetTime(), GameStateManager.Dificulty, "level1");
        }

        enabled = false;
    }
}
