using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float TransitionTime = 1;

    public static SceneLoader main;

    private void Awake()
    {
        main = this;
    }

    public void LoadLevel(string name)
    {
        StartCoroutine(LoadScene(name));
    }

    IEnumerator LoadScene(string name)
    {
        transition.SetTrigger("Fade");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(name);
    }
}
