using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSetterShader : MonoBehaviour
{
    Transform Object;
    public GameObject[] ObjectsToSet;

    List<Material> Mat = new List<Material>();
    void Awake()
    {
        foreach (GameObject obj in ObjectsToSet) 
            foreach (MeshRenderer rend in obj.GetComponentsInChildren<MeshRenderer>())
                foreach (Material m in rend.sharedMaterials)
                    if (!Mat.Contains(m) && m != null) Mat.Add(m);


    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.main.Player != null) 
            foreach (Material Obj in Mat)
                Obj.SetVector("_pos", Manager.main.Player.transform.position);
    }

    public void SetDistance(float dist)
    {
        foreach (Material Obj in Mat)
            Obj.SetFloat("_offset", dist);
    }
}
