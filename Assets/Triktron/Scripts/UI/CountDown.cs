using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public int Number = 1;

    Image Number1;
    Image Number2;
    Image Number3;
    Image NumberGO;

    float LastTime;
    public float TimerPerDiget = 2;
    void Start()
    {
        Number1 = gameObject.transform.Find("1").GetComponent<Image>();
        Number2 = gameObject.transform.Find("2").GetComponent<Image>();
        Number3 = gameObject.transform.Find("3").GetComponent<Image>();
        NumberGO = gameObject.transform.Find("go").GetComponent<Image>();
        Number = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (LastTime > TimerPerDiget)
        {
            LastTime = 0;
            Number++;
        }
        LastTime += Time.deltaTime;

        SetNumber();

        if (Number >= 5) enabled = false;
    }

    void SetNumber()
    {
        Number1.enabled = Number == 3;
        Number2.enabled = Number == 2;
        Number3.enabled = Number == 1;
        NumberGO.enabled = Number == 4;
    }
}
