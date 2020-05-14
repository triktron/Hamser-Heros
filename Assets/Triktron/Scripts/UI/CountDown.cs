using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public int Number = 1;

    public AudioClip Clip1;
    public AudioClip Clip2;

    AudioSource AS;

    Image Number1;
    Image Number2;
    Image Number3;
    Image NumberGO;

    float LastTime = float.MaxValue;
    public float TimerPerDiget = 2;
    void Start()
    {
        Number1 = gameObject.transform.Find("1").GetComponent<Image>();
        Number2 = gameObject.transform.Find("2").GetComponent<Image>();
        Number3 = gameObject.transform.Find("3").GetComponent<Image>();
        NumberGO = gameObject.transform.Find("go").GetComponent<Image>();
        Number = 0;

        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LastTime > TimerPerDiget)
        {
            LastTime = 0;
            Number++;

            SetNumber();
        }
        LastTime += Time.deltaTime;


        if (Number >= 5) enabled = false;
    }

    void SetNumber()
    {
        Number1.enabled = Number == 3;
        Number2.enabled = Number == 2;
        Number3.enabled = Number == 1;
        NumberGO.enabled = Number == 4;

        if (Number <= 4) AS.PlayOneShot(Number != 4 ? Clip1 : Clip2);
    }
}
