﻿using System.Collections;
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
    public RectTransform Sldier;
    public CanvasGroup MenuGroup;
    public InputSystemUIInputModule InputModule;
    public float TransitionTime = 0.4f;
    public LeanTweenType typ;
    [Header("main")]
    public GameObject ExitBtn;
    [Header("score")]
    public TMP_Text Scores;
    public Selector LevelSelector;
    public Selector ColorSelector;
    public Scrollbar Scrollbar;
    [Header("settings")]
    public TMP_InputField Nickname;
    public Text VRModeToggle;
    [Header("level")]
    public Text DificultyBar;
    public UpdateLevelSelector UpdateLevelSelector;
    public Text CoinCount;
    [Header("First Time")]
    public RectTransform FirstTimeWindow;
    public TMP_InputField NicknameFirst;



    RectTransform Canvas;

    public void OpenAchivements()
    {
        FindObjectOfType<AchievenmentListIngame>().Open();
    }

    private void Start()
    {
        if (GameStateManager.Username != "Anonymous") NicknameFirst.text = GameStateManager.Username;
        LoadScores();
        Canvas = GetComponent<RectTransform>();

        #if UNITY_WEBGL || UNITY_EDITOR
        ExitBtn.SetActive(false);
#endif

        updateSettingsUI();

        UpdateDificultyDisplay();

        UpdateCoinCount();

        if (GameStateManager.FistTime)
        {
            MenuGroup.alpha = 0;
            MenuGroup.interactable = false;
            FirstTimeWindow.localPosition = Vector3.zero;
            NicknameFirst.Select();
        }
    }

    void updateSettingsUI()
    {
        Nickname.text = GameStateManager.Username;
        VRModeToggle.text = "VR Mode is " + (GameStateManager.VRMode ? "ON" : "OFF");
    }

    public void ToggleVRMode()
    {
        GameStateManager.VRMode = !GameStateManager.VRMode;
        updateSettingsUI();
    }

    public void CloseFirst(bool save = true)
    {
        MenuGroup.alpha = 1;
        MenuGroup.interactable = true;
        FirstTimeWindow.localPosition = new Vector3(9000, 9000);
        SelectButton(ExitBtn.GetComponent<Button>());
        if (save) GameStateManager.FistTime = false;
    }

    public void CheckScrollbarPos()
    {
        Navigation nav = Scrollbar.navigation;
        nav.mode = Scrollbar.value == 1 ? Navigation.Mode.Explicit : Navigation.Mode.None;
        Scrollbar.navigation = nav;
    }

    public void LoadScores()
    {
        Highscores.instance.DownloadHighscores(() => UpdateScores());
    }

    public void UpdateScores()
    {
        StringBuilder str = new StringBuilder("");

        int s = 1;
        for (int i = 0; i < Highscores.instance.highscoresList.Length; i++)
        {
            if (Highscores.instance.highscoresList[i].level == LevelSelector.Index+1 && Highscores.instance.highscoresList[i].type == ColorSelector.Index+1)
            {
                str.AppendFormat("{0}. <b><color=\"red\">{1}<color=\"white\"></b> with {2} seconds on {3}\n", s, Highscores.instance.highscoresList[i].username, Highscores.instance.highscoresList[i].time.ToString("F3"), GameStateManager.GetDificultyName(Highscores.instance.highscoresList[i].dif));
                s++;
            }
        }

        Scores.SetText(str.ToString());

        RectTransform parrent = Scores.transform.parent.GetComponent<RectTransform>();

        parrent.sizeDelta = new Vector2(parrent.sizeDelta.x, Scores.preferredHeight);

    }

    public void SetNicknameFromSettigs()
    {
        GameStateManager.Username = Nickname.text;
    }

    public void SetNicknameFromFirst()
    {
        GameStateManager.Username = NicknameFirst.text;
        Nickname.text = NicknameFirst.text;
    }


    public void Log(string name)
    {
        Debug.Log(name);
    }

    public void UpdateDificultyDisplay()
    {
        DificultyBar.text = GameStateManager.GetDificultyName(GameStateManager.Dificulty);
    }

    public void UpdateCoinCount()
    {
        CoinCount.text = "You have " + GameStateManager.Coins +" coins";
    }

    int lastLeanId = -1;


    public void GoToPageHorizontal(int i)
    {
        // Sldier.localPosition = Vector3.left * i * 1920;
        InputModule.enabled = false;
        if (lastLeanId != -1) LeanTween.cancel(lastLeanId);
        lastLeanId = LeanTween.move(Sldier, Vector3.left * i * Canvas.rect.width, TransitionTime).setOnComplete(() => { InputModule.enabled = true; }).setEase(typ).id;
    }

    public void GoToPageVertical(int i)
    {
        // Sldier.localPosition = Vector3.left * i * 1920;
        InputModule.enabled = false;
        if (lastLeanId != -1) LeanTween.cancel(lastLeanId);
        lastLeanId = LeanTween.move(Sldier, Vector3.down * i * Canvas.rect.height, TransitionTime).setOnComplete(() => { InputModule.enabled = true; }).setEase(typ).id;
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
        
        LevelUnlockeble data = EventSystem.current.currentSelectedGameObject.GetComponent<LevelUnlockeble>();
        
        if (data.Unlocked)
        {
            AchievementManager.instance.AddAchievementProgress("games", 1);
            Debug.Log(data.Name);
            SceneLoader.main.LoadLevel("level" + data.Name);
        }
        else if (data.Selecteble)
        {
            Debug.Log("trying to buy");
            if (GameStateManager.Coins >= data.Cost)
            {
                GameStateManager.Coins -= data.Cost;
                PlayerPrefs.SetInt("Level" + data.Name + "Unlocked", 1);
                UpdateCoinCount();
                AchievementManager.instance.AddAchievementProgress("completed", 1);
                UpdateLevelSelector.UpdateUI();
            }
        }
    }
}
