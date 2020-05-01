using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    static public Dificulties Dificulty { 
        get
        {
            return (Dificulties)PlayerPrefs.GetInt("Difficulty", (int)Dificulties.medium); 
        }
        
        set
        {
            PlayerPrefs.SetInt("Difficulty", (int)value);
        }
    }

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

    public static bool FistTime
    {
        get
        {
            return PlayerPrefs.GetInt("FirstTime", 1) == 1;
        }

        set
        {
            PlayerPrefs.SetInt("FirstTime", value ? 1 : 0);
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
        CC,
        easy,
        medium,
        hard,
        expert
    }

    public void SetDificulty(int dificulty)
    {
        Dificulty = (Dificulties)dificulty;
    }

    public void IncreaseDificulty()
    {
        if ((int)Dificulty < (int)Dificulties.expert) Dificulty = (Dificulties)((int)Dificulty + 1);
    }
    public void DecreaseDificulty()
    {
        if ((int)Dificulty > 0) Dificulty = (Dificulties)((int)Dificulty - 1);
    }
    

    static public float GetDificultyValueDrag()
    {
        if (Dificulty == Dificulties.easy || Dificulty == Dificulties.CC) return 1f;
        if (Dificulty == Dificulties.medium) return 0.5f;
        if (Dificulty == Dificulties.hard) return 0.2f;
        return 0f;
    }

    static public float GetDificultyValueVisibilety()
    {
        if (Dificulty == Dificulties.easy) return -7f;
        if (Dificulty == Dificulties.medium || Dificulty == Dificulties.CC) return -2.3f;
        if (Dificulty == Dificulties.hard) return -1.5f;
        return -0.8f;
    }

    static public string GetDificultyName(Dificulties dificulty)
    {
        if (dificulty == Dificulties.CC) return "Character Controller";
        if (dificulty == Dificulties.easy) return "easy";
        if (dificulty == Dificulties.medium) return "medium";
        if (dificulty == Dificulties.hard) return "hard";
        return "expert";
    }
}
