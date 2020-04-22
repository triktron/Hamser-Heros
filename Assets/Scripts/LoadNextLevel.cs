using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
