using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    static public Dificulties Dificulty = Dificulties.medium;

    public static string Username
    {
        get
        {
            return PlayerPrefs.GetString("Name", "Anonymous");
        }

        set
        {
            PlayerPrefs.SetString("Name", value);
        }
    }

    public static string uuid
    {
        get
        {
            string id = PlayerPrefs.GetString("uuid", System.Guid.NewGuid().ToString().Substring(0, 8));
            PlayerPrefs.SetString("uuid", id);
            return id;
        }
    }

    public enum Dificulties
    {
        easy,
        medium,
        hard,
        expert
    }

    public void SetDificulty(int dificulty)
    {
        Dificulty = (Dificulties)dificulty;
    }

    static public float GetDificultyValueDrag()
    {
        if (Dificulty == Dificulties.easy) return 1f;
        if (Dificulty == Dificulties.medium) return 0.5f;
        if (Dificulty == Dificulties.hard) return 0.2f;
        return 0f;
    }

    static public float GetDificultyValueVisibilety()
    {
        if (Dificulty == Dificulties.easy) return -7f;
        if (Dificulty == Dificulties.medium) return -2.3f;
        if (Dificulty == Dificulties.hard) return -1.5f;
        return -0.8f;
    }

    static public string GetDificultyName(Dificulties dificulty)
    {
        if (dificulty == Dificulties.easy) return "easy";
        if (dificulty == Dificulties.medium) return "medium";
        if (dificulty == Dificulties.hard) return "hard";
        return "expert";
    }
}
