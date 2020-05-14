using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLevelSelector : MonoBehaviour
{
    private void Start()
    {
        UpdateUI();
    }


    public void UpdateUI()
    {
        int r = 0;
        foreach (Transform row in transform)
        {
            Button[] buttons = row.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                LevelUnlockeble data = buttons[i].GetComponent<LevelUnlockeble>();
                data.UpdateValues();
                buttons[i].interactable = data.Selecteble && !data.UnderConstruction;
                buttons[i].transform.GetChild(0).GetComponent<Image>().enabled = !data.Unlocked;
                buttons[i].transform.GetChild(1).GetComponent<Image>().enabled = data.Selecteble && !data.Unlocked;
                buttons[i].transform.GetChild(1).GetComponentInChildren<Text>().text = data.UnderConstruction ? "Under\nConstruction" : (data.Cost + " Coins");
                buttons[i].transform.GetChild(1).GetComponentInChildren<Text>().enabled = data.Selecteble && !data.Unlocked;
            }
            r++;
        }

        

        /*foreach (Button btn in transform.GetComponentsInChildren<Button>())
        {
            Navigation nav = btn.navigation;
            if (nav.selectOnLeft != null && !nav.selectOnLeft.interactable) nav.selectOnLeft = null;
            if (nav.selectOnRight != null && !nav.selectOnRight.interactable) nav.selectOnRight = null;
            if (nav.selectOnUp != null && !nav.selectOnUp.interactable) nav.selectOnUp = null;
            if (nav.selectOnDown != null && !nav.selectOnDown.interactable) nav.selectOnDown = null;
            btn.navigation = nav;
        }*/
    }
}
