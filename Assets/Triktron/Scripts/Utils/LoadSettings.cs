using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    public PhysicMaterial Material;
    public GameObject Player;
    public GameObject PlayerCC;
    public Transform PlayerStartPoint;
    void Awake()
    {
        Material.dynamicFriction = GameStateManager.GetDificultyValueDrag();
        Material.staticFriction = GameStateManager.GetDificultyValueDrag();
    }

    public void Load()
    {
        Debug.Log("loading settings");
        PositionSetterShader shader = GetComponent<PositionSetterShader>();

        if (shader != null)
        {
            shader.SetDistance(GameStateManager.GetDificultyValueVisibilety());
        }

        if (Manager.main.Player != null) Destroy(Manager.main.Player);

        Manager.main.Player = Instantiate(GameStateManager.Dificulty == GameStateManager.Dificulties.CC ? PlayerCC : Player);
        Manager.main.Player.transform.position = PlayerStartPoint.position;
        Manager.main.Player.transform.rotation = PlayerStartPoint.rotation;

        Manager.main.StartNextState();
    }
}
