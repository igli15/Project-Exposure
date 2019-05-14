using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Path myScript = (Path)target;

        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.red;
        
        if (GUILayout.Button("Generate Points"))
        {
            myScript.GeneratePoints();
        }
        if (GUILayout.Button("Add Point"))
        {
            myScript.AddPoint();
        }
        GUILayout.Space(30);
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Clear Points"))
        {
            if ( EditorUtility.DisplayDialog("","Are you sure you want to clear path points???", "yes", "NO"))
            {
                myScript.DestroyPoints();
            }
        }
    }

    public static T SafeDestroy<T>(T obj) where T : Object
    {
        if (Application.isEditor)
            Object.DestroyImmediate(obj);
        else
            Object.Destroy(obj);

        return null;
    }
    public static T SafeDestroyGameObject<T>(T component) where T : Component
    {
        if (component != null)
            SafeDestroy(component.gameObject);
        return null;
    }

}
