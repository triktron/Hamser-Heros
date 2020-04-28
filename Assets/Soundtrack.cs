using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    public AudioClip[] Soundtracks;
    void Start()
    {
        GetComponent<AudioSource>().clip = Soundtracks[Random.Range(0, Soundtracks.Length)];
        GetComponent<AudioSource>().Play();
    }
}
