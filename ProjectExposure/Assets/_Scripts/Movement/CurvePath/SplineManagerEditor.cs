using System;
#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;


[CustomEditor(typeof(SplineManager))]
public class SplineManagerEditor : Editor
{
    SplineManager m_manager;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        m_manager = target as SplineManager;

        //GUILayout.Label("DrawInEditor");
    }

}

#endif