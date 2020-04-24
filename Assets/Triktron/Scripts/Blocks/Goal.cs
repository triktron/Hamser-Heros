using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

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

        Highscores.AddNewHighscore(GameStateManager.Username, Timer.GetTime(), GameStateManager.Dificulty, int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5)));
        

        enabled = false;
    }
}
