﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    public float PlaneHeight = -100;
    public float animationHeight = 50;
    public CameraFollowController cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < PlaneHeight)
        {
            cam.Stationary = true;

            if (transform.position.y < PlaneHeight - animationHeight) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
