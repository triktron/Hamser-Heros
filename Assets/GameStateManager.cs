using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    static public Dificulties Dificulty = Dificulties.medium;

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
    
    static public float GetDificultyValue()
    {
        if (Dificulty == Dificulties.easy)   return 1f;
        if (Dificulty == Dificulties.medium) return 0.5f;
        if (Dificulty == Dificulties.hard)   return 0.2f;
                                             return 0f;
    }
}
