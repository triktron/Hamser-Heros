using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class DataManager : EditorWindow
{

    // Add menu named "My Window" to the Window menu
    [MenuItem("tools/Data Manager")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        DataManager window = (DataManager)EditorWindow.GetWindow(typeof(DataManager));
        Debug.Log("opening data manager");

        window.RawData = UpdateData();
        window.Unlocked = UpdateUnlocked();
        window.Show();
    }

    RawPref[] RawData;

    static RawPref[] UpdateData()
    {
        return new RawPref[]{
            new RawPref("Dificulty", RawPref.Types.Int),
            new RawPref("Name", RawPref.Types.String),
            new RawPref("FistTime", RawPref.Types.Bool),
            new RawPref("VRMode", RawPref.Types.Bool),
            new RawPref("Coins", RawPref.Types.Int),
            new RawPref("uuid", RawPref.Types.String),
            new RawPref("MasterVol", RawPref.Types.Float),
            new RawPref("SoundtrackVol", RawPref.Types.Float)
        };
    }

    static bool[,] UpdateUnlocked()
    {
        bool[,] Unlocked = new bool[3, 6];
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                Unlocked[x, y] = PlayerPrefs.GetInt("Level" + (x + 1) + "-" + (y + 1) + "Unlocked", 0) == 1;
            }
        }

        return Unlocked;
    }


    bool ShowRaw;
    void OnGUI()
    {
        if (GUILayout.Button("Update") || RawData == null || Unlocked == null)
        {
            RawData = UpdateData();
            Unlocked = UpdateUnlocked();
        }

        ShowRaw = EditorGUILayout.Foldout(ShowRaw, "Raw Data");
        if (ShowRaw)
        {
            foreach (RawPref pref in RawData)
            {
                EditorGUI.BeginChangeCheck();
                switch (pref.Type)
                {
                    case RawPref.Types.String:
                        pref.data = EditorGUILayout.TextField(pref.Name, pref.data as string);
                        break;
                    case RawPref.Types.Float:
                        pref.data = EditorGUILayout.FloatField(pref.Name, (float)pref.data);
                        break;
                    case RawPref.Types.Int:
                        pref.data = EditorGUILayout.IntField(pref.Name, (int)pref.data);
                        break;
                    case RawPref.Types.Bool:
                        pref.data = EditorGUILayout.Toggle(pref.Name, (bool)pref.data);
                        break;
                }
                if (EditorGUI.EndChangeCheck()) pref.UpdateValue();
            }
        }

        UnlockedLevels();
    }

    bool ShowUnlocked;
    bool[,] Unlocked = new bool[3,6];
    void UnlockedLevels()
    {
        ShowUnlocked = EditorGUILayout.Foldout(ShowUnlocked, "Unlocked Levels");
        if (ShowUnlocked)
        {
            for (int x = 0; x < 3; x++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int y = 0; y < 6; y++)
                {
                    EditorGUI.BeginChangeCheck();
                    Unlocked[x,y] = EditorGUILayout.Toggle(Unlocked[x,y]);
                    if (EditorGUI.EndChangeCheck()) PlayerPrefs.SetInt("Level" + (x+1) + "-" + (y+1) + "Unlocked", Unlocked[x, y] ? 1 : 0);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    public class RawPref {
        public enum Types
        {
            String,
            Float,
            Int,
            Bool
        }

        public Types Type;
        public string Name;
        public object data;

        public RawPref(string name, Types type)
        {
            Name = name;
            Type = type;

            switch (Type)
            {
                case RawPref.Types.String:
                    data = PlayerPrefs.GetString(name);
                    break;
                case RawPref.Types.Float:
                    data = PlayerPrefs.GetFloat(name);
                    break;
                case RawPref.Types.Int:
                    data = PlayerPrefs.GetInt(name);
                    break;
                case RawPref.Types.Bool:
                    data = PlayerPrefs.GetInt(name) == 1;
                    break;
            }
        }

        public void UpdateValue()
        {
            switch (Type)
            {
                case RawPref.Types.String:
                    PlayerPrefs.SetString(Name, data as string);
                    break;
                case RawPref.Types.Float:
                    PlayerPrefs.SetFloat(Name, (float)data);
                    break;
                case RawPref.Types.Int:
                    PlayerPrefs.SetInt(Name, (int)data);
                    break;
                case RawPref.Types.Bool:
                    PlayerPrefs.SetInt(Name, (bool)data ? 1 : 0);
                    break;
            }
        }
    }

}