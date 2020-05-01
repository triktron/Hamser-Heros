using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

    bool Reached;
    private void OnTriggerEnter(Collider other)
    {
        if (!Reached)
        {
            Reached = true;
            //Timer.StopTimer();

#if !UNITY_EDITOR
        Analytics.CustomEvent("level_completed", new Dictionary<string, object>
        {
            { "time_elapsed", Timer.GetTime() },
            { "level",  SceneManager.GetActiveScene().name},
            { "Dificulty", GameStateManager.GetDificultyName(GameStateManager.Dificulty) },
            { "Tries", TriesCount.Tries }
        });

        string[] levelAndType = SceneManager.GetActiveScene().name.Remove(0, 5).Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries);

        Highscores.AddNewHighscore(GameStateManager.Username, Timer.GetTime(), GameStateManager.Dificulty, int.Parse(levelAndType[1]), int.Parse(levelAndType[0]));
#endif

            //TriesCount.Tries = 0;

            Manager.main.StartNextState();
            enabled = false;
        }
        
    }
}
