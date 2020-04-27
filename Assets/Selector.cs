using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public string[] Options;
    public int Index;

    Button PrevBtn;
    Button NextBtn;
    Text Middle;

    public UnityEvent OnChange;

    void Start()
    {
        PrevBtn = transform.Find("prev").GetComponent<Button>();
        NextBtn = transform.Find("next").GetComponent<Button>();
        Middle = transform.Find("middle").GetComponentInChildren<Text>();

        PrevBtn.onClick.AddListener(() => Prev());
        NextBtn.onClick.AddListener(() => Next());

        UpdateText();
    }

    public void Prev()
    {
        if (Index > 0) Index--;
        UpdateText();
        OnChange.Invoke();
    }

    public void Next()
    {
        if (Index < Options.Length - 1) Index++;
        UpdateText();
        OnChange.Invoke();
    }

    void UpdateText()
    {
        Middle.text = Options[Index];
    }
}
