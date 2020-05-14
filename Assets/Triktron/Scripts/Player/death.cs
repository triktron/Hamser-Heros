using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{
    public float PlaneHeight = -20;
    public float animationHeight = 50;
    public CameraFollowController cam;
    bool achivementAdded;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < PlaneHeight)
        {
            if (!achivementAdded)
            {
                achivementAdded = true;
                if (AchievementManager.instance != null) AchievementManager.instance.AddAchievementProgress("died", 1);
            }
            if (Manager.main.Player && Manager.main.Player.GetComponent<PlayerController>() != null && Manager.main.Player.GetComponent<PlayerController>().Cam) Manager.main.Player.GetComponent<PlayerController>().Cam.Stationary = true;

            if (transform.position.y < PlaneHeight - animationHeight) SceneLoader.main.LoadLevel(SceneManager.GetActiveScene().name);
        }
    }
}
