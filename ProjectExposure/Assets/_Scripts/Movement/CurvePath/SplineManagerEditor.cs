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

        EditorGUI.BeginChangeCheck();
        GUILayoutOption[] opt = { GUILayout.Height(60) };
        if (GUILayout.Button("Activate all elements",opt))
        {
            SubscribeToUpdate(); 
            m_manager.UpdateSplineList();
        }
    }

    public void SubscribeToUpdate()
    {
        if (SplineManager.SubscribedToUpdate == false)
        {
            Debug.Log("Subscribing to Editor Update");
            EditorApplication.update += OnCustomSceneGUI;
            SplineManager.SubscribedToUpdate = true;
        }
    }

    

    void OnCustomSceneGUI()
    {
        //Debug.Log("OnCustomSceneGUI");
    }

    private void OnDisable()
    {
        if (SplineManager.SubscribedToUpdate == true)
        {
            Debug.Log("Unsubscribing from Editor Update");
            EditorApplication.update -= OnCustomSceneGUI;
            SplineManager.SubscribedToUpdate = false;
        }
    }

}

#endif