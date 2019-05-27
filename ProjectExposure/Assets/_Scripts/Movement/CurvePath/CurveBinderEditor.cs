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
        val = GUILayout.HorizontalSlider(curveBinder.progress, 0, 1);
        curveBinder.StayAtSpline(val);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(curveBinder, "curveBinder");
            EditorUtility.SetDirty(curveBinder);
            curveBinder.progress = val;
        }
    }
}
