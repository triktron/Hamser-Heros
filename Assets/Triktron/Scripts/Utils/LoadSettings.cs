using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    public PhysicMaterial Material;
    void Awake()
    {
        Material.dynamicFriction = GameStateManager.GetDificultyValueDrag();
        Material.staticFriction = GameStateManager.GetDificultyValueDrag();
    }

    private void Start()
    {
        PositionSetterShader shader = GetComponent<PositionSetterShader>();

        if (shader != null)
        {
            shader.SetDistance(GameStateManager.GetDificultyValueVisibilety());
        }
    }
}
