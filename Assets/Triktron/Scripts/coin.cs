using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameStateManager.Coins++;
        if (AchievementManager.instance != null) AchievementManager.instance.AddAchievementProgress("coin", 1);
        Destroy(gameObject);
    }
}
