using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    public PhysicMaterial Material;
    void Awake()
    {
        Material.dynamicFriction = GameStateManager.GetDificultyValue();
        Material.staticFriction = GameStateManager.GetDificultyValue();
    }
}
