using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : EditorWindow
{
    public Transform Root;
    public GameObject LevelPrefab;
    public Button TopRowSelect;

    public Row[] Rows;

    [MenuItem("tools/Level icon Selector")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LevelSelector window = (LevelSelector)EditorWindow.GetWindow(typeof(LevelSelector));
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

            EditorGUILayout.PropertyField(obj.FindProperty("Rows"));
            EditorGUILayout.PropertyField(obj.FindProperty("LevelPrefab"));
            EditorGUILayout.PropertyField(obj.FindProperty("TopRowSelect"));
            if (GUILayout.Button("Update")) Place();

        }

        obj.ApplyModifiedProperties();
    }

    void Place()
    {
        while (Root.childCount != 0)
        {
            DestroyImmediate(Root.GetChild(0).gameObject);
        }

        foreach (Row row in Rows)
        {
            GameObject container = new GameObject();
            container.transform.SetParent(Root);

            HorizontalLayoutGroup layout = container.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 30;
            layout.childControlHeight = false;
            layout.childControlWidth = false;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;



            for (int i = 0; i < row.Items; i++)
            {
                GameObject level = Instantiate(LevelPrefab, container.transform);
                Button btn = level.GetComponent<Button>();
                var colors = btn.colors;
                colors.normalColor = row.Color;
                btn.colors = colors;

                btn.interactable = i <= row.Unlocked;
                level.transform.GetChild(0).GetComponent<Image>().enabled = i > row.Unlocked;
            }
        }

        foreach (Button btn in Root.GetComponentsInChildren<Button>())
        {
            Navigation nav = new Navigation();
            nav.mode = Navigation.Mode.Explicit;
            Transform parrent = btn.gameObject.transform.parent.transform;
            int sublingindex = btn.gameObject.transform.GetSiblingIndex();
            int sublingindexparrent = parrent.GetSiblingIndex();
            if (sublingindex > 0) nav.selectOnLeft = parrent.GetChild(sublingindex - 1).GetComponent<Button>();
            if (sublingindex < parrent.childCount - 1) nav.selectOnRight = parrent.GetChild(sublingindex + 1).GetComponent<Button>();
            if (sublingindexparrent > 0 && Root.GetChild(sublingindexparrent-1).childCount >= sublingindex) nav.selectOnUp = Root.GetChild(sublingindexparrent - 1).GetChild(sublingindex).GetComponent<Button>();
            if (sublingindexparrent < Root.childCount - 1 && Root.GetChild(sublingindexparrent+1).childCount >= sublingindex) nav.selectOnDown = Root.GetChild(sublingindexparrent + 1).GetChild(sublingindex).GetComponent<Button>();
            if (TopRowSelect != null && sublingindexparrent == 0) nav.selectOnUp = TopRowSelect;
            btn.navigation = nav;
        }
    }

    [System.Serializable]
    public class Row
    {
        public int Items;
        public int Unlocked;
        public Color Color;
    }
}
