﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Path myScript = (Path)target;
        if (GUILayout.Button("Generate Points"))
        {
            myScript.GeneratePoints();
        }
        if (GUILayout.Button("Clear Points"))
        {
            myScript.DestroyPoints();
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
