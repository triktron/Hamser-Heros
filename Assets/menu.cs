using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update

    public RectTransform Sldier;
    public InputSystemUIInputModule InputModule;
    public void Log(string name)
    {
        Debug.Log(name);
    }

    public LeanTweenType typ;
    public float TransitionTime = 0.4f;

    int lastLeanId = -1;


    public void GoToPageHorizontal(int i)
    {
        // Sldier.localPosition = Vector3.left * i * 1920;
        InputModule.enabled = false;
        if (lastLeanId != -1) LeanTween.cancel(lastLeanId);
        lastLeanId = LeanTween.move(Sldier, Vector3.left * i * 1920, TransitionTime).setOnComplete(() => { InputModule.enabled = true; }).setEase(typ).id;
    }

    public void GoToPageVertical(int i)
    {
        // Sldier.localPosition = Vector3.left * i * 1920;
        InputModule.enabled = false;
        if (lastLeanId != -1) LeanTween.cancel(lastLeanId);
        lastLeanId = LeanTween.move(Sldier, Vector3.down * i * 1080, TransitionTime).setOnComplete(() => { InputModule.enabled = true; }).setEase(typ).id;
    }

    public void Exit()
    {
        if (lastLeanId != -1) LeanTween.cancel(lastLeanId);
        lastLeanId = LeanTween.move(Sldier, Vector3.up * 1920, TransitionTime).setOnComplete(() => { Application.Quit(); }).setEase(typ).id;
    }

    public void SelectButton(Button btn)
    {
        btn.Select();
    }

    public void openLevel()
    {
        SceneManager.LoadScene("level1", LoadSceneMode.Single);
    }
}
