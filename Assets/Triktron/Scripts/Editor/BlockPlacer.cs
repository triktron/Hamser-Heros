﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class BlockPlacer : EditorWindow
{
    public Transform Root;

    Vector3 CurrentPos;
    Vector3 Heading = Vector3.forward;


    // Add menu named "My Window" to the Window menu
    [MenuItem("tools/Block Placer")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        BlockPlacer window = (BlockPlacer)EditorWindow.GetWindow(typeof(BlockPlacer));
        window.Show();
    }

    void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("Root"));

        if (Root == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected. please assign a root transform.", MessageType.Warning);
        }
        else
        {
            
            DrawButtons(obj);
            
        }

        obj.ApplyModifiedProperties();
    }

    bool tilted;
    bool prevTilted;

    private void DrawButtons(SerializedObject obj)
    {
        EditorGUILayout.BeginVertical("box");

        if (GUILayout.Button("remove")) remove();
        if (GUILayout.Button("forward")) placeBlockForward();
        if (GUILayout.Button("left")) placeBlockLeft();
        if (GUILayout.Button("right")) placeBlockRight();
        if (GUILayout.Button("up")) placeBlockUp();
        if (GUILayout.Button("down")) placeBlockDown();

        tilted = EditorGUILayout.Toggle("tilt", tilted);

        if (GUILayout.Button("toggle hidden")) ToggleHidden();

            EditorGUILayout.EndVertical();
    }

    private void ToggleHidden()
    {
        //PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeGameObject).getpath
        Debug.Log(AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeGameObject)).Contains("normal"));

        bool ToHidden = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(Selection.activeGameObject)).Contains("normal");

        for (var i = Selection.gameObjects.Length - 1; i >= 0; --i)
        {
            //List<UnityEditor.SceneManagement.AddedComponent> added = PrefabUtility.GetAddedComponents(obj.gameObject);

            string path = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(Selection.gameObjects[i]));
            if (ToHidden) path = path.Replace("normal", "hidden"); else path = path.Replace("hidden", "normal");

            Debug.Log(path.Remove(path.Length - 7, 7).Remove(0,26)); //Assets / Triktron / Resources / hidden / straight.prefab
            GameObject road = PrefabUtility.InstantiatePrefab(Resources.Load(path.Remove(path.Length - 7, 7).Remove(0, 26), typeof(GameObject)) as GameObject) as GameObject;

            Undo.RegisterCreatedObjectUndo(road, "Replace With Prefabs");
            road.transform.parent = Selection.gameObjects[i].transform.parent;
            road.transform.localPosition = Selection.gameObjects[i].transform.localPosition;
            road.transform.localRotation = Selection.gameObjects[i].transform.localRotation;
            road.transform.localScale = Selection.gameObjects[i].transform.localScale;
            road.transform.SetSiblingIndex(Selection.gameObjects[i].transform.GetSiblingIndex());
            Undo.DestroyObjectImmediate(Selection.gameObjects[i]);
        }
    }

    private void placeBlockDown()
    {
        CurrentPos += Heading * 5 - Vector3.up * 2.2813f;
        GameObject road = PrefabUtility.InstantiatePrefab(Resources.Load("normal/rasie down", typeof(GameObject)) as GameObject, Root) as GameObject;

        road.transform.position = CurrentPos;
        road.transform.rotation = Quaternion.LookRotation(Heading, Vector3.up) * Quaternion.Euler(-0, 0, -0);

    }

    private void placeBlockUp()
    {
        CurrentPos += Heading * 5;
        GameObject road = PrefabUtility.InstantiatePrefab(Resources.Load("normal/raise up", typeof(GameObject)) as GameObject, Root) as GameObject;

        road.transform.position = CurrentPos;
        road.transform.rotation = Quaternion.LookRotation(Heading, Vector3.up) * Quaternion.Euler(-0, 180, -0);

        CurrentPos += Vector3.up * 2.2813f;
    }

    private void placeBlockRight()
    {
        CurrentPos += Heading * 5;
        Heading = Quaternion.Euler(0, 90, 0) * Heading;
        GameObject road = PrefabUtility.InstantiatePrefab(Resources.Load(tilted ? "normal/curve_tilt" : "normal/bent", typeof(GameObject)) as GameObject, Root) as GameObject;

        road.transform.position = CurrentPos;
        road.transform.rotation = Quaternion.LookRotation(Heading, Vector3.up) * Quaternion.Euler(-0, -90, -0);
        road.transform.localScale = new Vector3(-1,1, tilted ? -1 : 1);

        prevTilted = tilted;
    }

    private void remove()
    {
        DestroyImmediate(Root.transform.GetChild(Root.transform.childCount - 1).gameObject);
        CurrentPos = Root.transform.GetChild(Root.transform.childCount - 1).position;
        Heading = Root.transform.GetChild(Root.transform.childCount - 1).forward;

        prevTilted = Root.transform.GetChild(Root.transform.childCount - 1).name.Contains("tilt");
    }

    private void placeBlockLeft()
    {
        CurrentPos += Heading * 5;
        Heading = Quaternion.Euler(0,-90,0) * Heading; 
        GameObject road = PrefabUtility.InstantiatePrefab(Resources.Load(tilted ? "normal/curve_tilt" : "normal/bent", typeof(GameObject)) as GameObject, Root) as GameObject;

        road.transform.position = CurrentPos;
        road.transform.rotation = Quaternion.LookRotation(Heading, Vector3.up) * Quaternion.Euler(-0, tilted ? 180 : 90, -0);
        if (tilted) road.transform.localScale = new Vector3(1, 1, -1);
        prevTilted = tilted;
    }

    private void placeBlockForward()
    {
        CurrentPos += Heading * 5;
        GameObject road = PrefabUtility.InstantiatePrefab(Resources.Load(tilted ? "normal/tilt" : "normal/straight", typeof(GameObject)) as GameObject,Root) as GameObject;

        road.transform.position = CurrentPos;
        road.transform.rotation = Quaternion.LookRotation(Heading, Vector3.up) * Quaternion.Euler(-0,0,-0);
        prevTilted = tilted;
    }
}