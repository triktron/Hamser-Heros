using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsTest : MonoBehaviour
{
    public Text vrmode;
    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        vrmode.text = "VR Mode is " + (GameStateManager.VRMode ? "ON" : "OFF");
    }

    public void ToggleVRMode()
    {
        GameStateManager.VRMode = !GameStateManager.VRMode;
        UpdateUI();
    }
}
