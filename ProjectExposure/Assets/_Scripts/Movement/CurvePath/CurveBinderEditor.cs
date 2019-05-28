using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CurveBinder))]
public class CurveBinderEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CurveBinder curveBinder = target as CurveBinder;
        float val;
        EditorGUI.BeginChangeCheck();
        GUILayout.Label("StartValue");
        val = GUILayout.HorizontalSlider(curveBinder.startProgress, 0, 1);
        curveBinder.AttachStartNode(val);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(curveBinder, "curveBinder");
            EditorUtility.SetDirty(curveBinder);
            curveBinder.startProgress = val;
        }
        float val2;
        EditorGUI.BeginChangeCheck();
        GUILayout.Label("EndValue");
        val2 = GUILayout.HorizontalSlider(curveBinder.endPprogress, 0, 1);
        curveBinder.AttachEndNode(val2);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(curveBinder, "curveBinder2");
            EditorUtility.SetDirty(curveBinder);
            curveBinder.endPprogress = val2;
        }

        ////🤬🤬🤬🤬🤬🤬🤬🤬🤬🤬🤬🤬🤬🤬🤬🤬
    }
}
