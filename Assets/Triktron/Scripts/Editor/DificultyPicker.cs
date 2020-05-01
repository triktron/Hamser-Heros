using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class DificultyPicker : EditorWindow
{
    public Transform Root;

    // Add menu named "My Window" to the Window menu
    [MenuItem("tools/Difficulty Picker")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        DificultyPicker window = (DificultyPicker)EditorWindow.GetWindow(typeof(DificultyPicker));
        window.Show();
    }

    float dist;

    void OnGUI()
    {
        EditorGUI.BeginChangeCheck();
        GameStateManager.Dificulty = (GameStateManager.Dificulties)EditorGUILayout.EnumPopup("current", GameStateManager.Dificulty);

        if (EditorGUI.EndChangeCheck())
            foreach (var obj in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
                foreach (var comp in obj.GetComponentsInChildren<ShowOnDificulty>())
                    comp.Check();

        EditorGUI.BeginChangeCheck();
        dist = EditorGUILayout.Slider(dist, 0, 100);
        if (EditorGUI.EndChangeCheck())
        {
            List<Material> armat = new List<Material>();

            Renderer[] arrend = (Renderer[])Resources.FindObjectsOfTypeAll(typeof(Renderer));
            foreach (Renderer rend in arrend)
            {
                foreach (Material mat in rend.sharedMaterials)
                {
                        armat.Add(mat);
                }
            }

            foreach (Material mat in armat)
            {
                if (mat != null && mat.shader != null && mat.shader.name != null && mat.shader.name == "Shader Graphs/distanceShader")
                {
                    mat.SetFloat("_offset", -dist);
                }
            }
        }


    }
}