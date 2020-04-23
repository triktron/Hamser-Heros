using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShowOnDificulty : MonoBehaviour
{

    public bool Show;

    public GameStateManager.Dificulties Dificulty;

    private void Awake()
    {
        Check();
    }

    public void Check()
    {
        bool should = (int)Dificulty <= (int)GameStateManager.Dificulty;
        if (Show) should = (int)Dificulty >= (int)GameStateManager.Dificulty;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(should);
            }
        
    }
}