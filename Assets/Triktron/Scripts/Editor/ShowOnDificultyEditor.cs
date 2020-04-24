using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShowOnDificulty)), CanEditMultipleObjects]
public class ShowOnDificultyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as ShowOnDificulty;

        EditorGUI.BeginChangeCheck();

        myScript.Show = GUILayout.Toggle(myScript.Show, "Show");

        if (myScript.Show)
        {
            myScript.Dificulty = (GameStateManager.Dificulties)EditorGUILayout.EnumPopup("Show On Or Lower", myScript.Dificulty);
        } else
        {
            myScript.Dificulty = (GameStateManager.Dificulties)EditorGUILayout.EnumPopup("Hide On Or Lower", myScript.Dificulty);
        }

        if (EditorGUI.EndChangeCheck())
        {


            foreach (var obj in Selection.gameObjects)
            {
                ShowOnDificulty script = obj.GetComponent<ShowOnDificulty>();
                script.Show = myScript.Show;
                script.Dificulty = myScript.Dificulty;
                script.Check();
            }
        }

    }
}