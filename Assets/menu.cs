using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public TMP_InputField Nickname;
    public TMP_Text Scores;

    private void Start()
    {
        Nickname.text = GameStateManager.Username;
        Highscores.instance.DownloadHighscores(UpdateScores);
    }

    public void UpdateScores()
    {
        StringBuilder str = new StringBuilder("");

        for (int i = 0; i < Highscores.instance.highscoresList.Length; i++)
        {
            str.AppendFormat("{0}. <b><color=\"red\">{1}<color=\"white\"></b> with {2} seconds on {3}\n", i + 1, Highscores.instance.highscoresList[i].username, Highscores.instance.highscoresList[i].time.ToString("F3"), GameStateManager.GetDificultyName(Highscores.instance.highscoresList[i].dif));
        }

        Scores.SetText(str.ToString());

        RectTransform parrent = Scores.transform.parent.GetComponent<RectTransform>();

        parrent.sizeDelta = new Vector2(parrent.sizeDelta.x, Scores.preferredHeight);

    }

    public void SetNickname()
    {
        GameStateManager.Username = Nickname.text;
    }

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
        lastLeanId = LeanTween.move(Sldier, Vector3.left * i * Screen.width, TransitionTime).setOnComplete(() => { InputModule.enabled = true; }).setEase(typ).id;
    }

    public void GoToPageVertical(int i)
    {
        // Sldier.localPosition = Vector3.left * i * 1920;
        InputModule.enabled = false;
        if (lastLeanId != -1) LeanTween.cancel(lastLeanId);
        lastLeanId = LeanTween.move(Sldier, Vector3.down * i * Screen.height, TransitionTime).setOnComplete(() => { InputModule.enabled = true; }).setEase(typ).id;
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
