using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public Slider slider2;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        slider2.value = PlayerPrefs.GetFloat("SoundtrackVol", 0.75f);

        mixer.SetFloat("MasterVol", Mathf.Log10(slider.value) * 20);
        mixer.SetFloat("SoundtrackVol", Mathf.Log10(slider2.value) * 20);
    }

    bool IsDragging;
    public void OnEndDrag()
    {
        Debug.Log("drag end");
        IsDragging = false;
    }

    public void OnBeginDrag()
    {
        Debug.Log("drag start");

        IsDragging = true;
    }

    public void SetLevelMaster()
    {
        if (IsDragging)
        {
            Debug.Log("Setting auio level to" + slider.value);
            float sliderValue = slider.value;
            mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("MasterVol", sliderValue);
        }
    }

    public void SetLevelSoundtrack()
    {
        if (IsDragging)
        {
            float slider2Value = slider2.value;
            mixer.SetFloat("SoundtrackVol", Mathf.Log10(slider2Value) * 20);
            PlayerPrefs.SetFloat("SoundtrackVol", slider2Value);
        }
    }
}