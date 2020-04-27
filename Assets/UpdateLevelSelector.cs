using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] unlocked;
    void Start()
    {
        int r = 0;
        foreach (Transform row in transform)
        {
            Button[] buttons = row.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length - 1; i++)
            {
                buttons[i].interactable = unlocked[r] >= i;
                buttons[i].transform.GetChild(0).GetComponent<Image>().enabled = unlocked[r] < i;
            }
            r++;
        }

        

        foreach (Button btn in transform.GetComponentsInChildren<Button>())
        {
            Navigation nav = btn.navigation;
            if (nav.selectOnLeft != null && !nav.selectOnLeft.interactable) nav.selectOnLeft = null;
            if (nav.selectOnRight != null && !nav.selectOnRight.interactable) nav.selectOnRight = null;
            if (nav.selectOnUp != null && !nav.selectOnUp.interactable) nav.selectOnUp = null;
            if (nav.selectOnDown != null && !nav.selectOnDown.interactable) nav.selectOnDown = null;
            btn.navigation = nav;
        }
    }
}
