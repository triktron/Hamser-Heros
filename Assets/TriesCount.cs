using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriesCount : MonoBehaviour
{
    public static int Tries
    {
        get
        {
            return PlayerPrefs.GetInt("TriesFor" + SceneManager.GetActiveScene().name, 0);
        }
        set
        {
            PlayerPrefs.SetInt("TriesFor" + SceneManager.GetActiveScene().name, value);
        }
    }

    public void Start()
    {
        Tries++;
        GetComponent<TMP_Text>().text = Tries + " Tries";
    }
}
