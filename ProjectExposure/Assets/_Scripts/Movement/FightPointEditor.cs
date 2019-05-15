using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(FightPoint))]
public class FightPointEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FightPoint myScript = (FightPoint)target;

        if (GUILayout.Button("Generate EnemyPaths"))
        {
            myScript.GenerateEnemyPaths();
        }
        GUILayout.Space(30);
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clear EnemyPaths"))
        {
            if (EditorUtility.DisplayDialog("", "Are you sure you want to clear EnemyPaths???", "yes", "NO"))
            {
                myScript.DestroyEnemyPaths();
            }
        }
    }
}
