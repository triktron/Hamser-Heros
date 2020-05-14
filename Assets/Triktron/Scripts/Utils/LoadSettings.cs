using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class LoadSettings : MonoBehaviour
{
    public PhysicMaterial Material;
    public GameObject Player;
    public GameObject PlayerCC;
    public GameObject VR;
    public Transform PlayerStartPoint;
    void Awake()
    {
        Material.dynamicFriction = GameStateManager.GetDificultyValueDrag();
        Material.staticFriction = GameStateManager.GetDificultyValueDrag();
    }

    public void Load()
    {
        Debug.Log("loading settings");

        PositionSetterShader shader = FindObjectOfType<PositionSetterShader>();

        if (shader != null)
        {
            shader.SetDistance(GameStateManager.GetDificultyValueVisibilety());
        }

        if (Manager.main.Player != null) Destroy(Manager.main.Player);

        Manager.main.Player = Instantiate(GameStateManager.Dificulty == GameStateManager.Dificulties.CC ? PlayerCC : Player);
        Manager.main.Player.transform.position = PlayerStartPoint.position;
        Manager.main.Player.transform.rotation = PlayerStartPoint.rotation;

        if (GameStateManager.VRMode) LoadVR();

        Timer.ResetTimer();

        Manager.main.StartNextState();
    }

    public void LoadVR()
    {
        StartCoroutine(SwitchToVR());
    }

    public void UnLoadVR()
    {
        if (UnityEngine.XR.XRSettings.enabled) StartCoroutine(SwitchOutOfVr());
    }

    IEnumerator SwitchToVR()
    {
        Transform target = Camera.main.transform;
        Camera.main.GetComponent<CameraFollowController>().disable = true;
        Camera.main.enabled = false;

        if (!UnityEngine.XR.XRSettings.enabled)
        {
            UnityEngine.XR.XRSettings.LoadDeviceByName("OpenVR");

            // Wait one frame!
            yield return null;

            // Now it's ok to enable VR mode.
            UnityEngine.XR.XRSettings.enabled = true;

            yield return null;
        }


            GameObject vr = Instantiate(VR);

        vr.GetComponent<CameraFollowController>().Awaken(Manager.main.Player.transform);
        Camera vrcam = vr.GetComponentInChildren<Camera>();
        Manager.main.Player.GetComponent<PlayerController>().CamTransform = vrcam.transform;

        foreach (Canvas c in GameObject.FindObjectsOfType<Canvas>()) c.worldCamera = vrcam;
    }
    IEnumerator SwitchOutOfVr()
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName("");

        // Wait one frame!
        yield return null;

        Destroy(GameObject.Find("VR"));

        yield return null;

        FindObjectOfType<Camera>().enabled = true;
        Camera.main.GetComponent<CameraFollowController>().disable = false;

        Camera cam = Camera.main;

        foreach (Canvas c in GameObject.FindObjectsOfType<Canvas>()) c.worldCamera = cam;

    }
}
