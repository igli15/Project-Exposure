using System;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CurvePoint))]
public class CurvePointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CurvePoint curvePoint = target as CurvePoint;
        float val;
        EditorGUI.BeginChangeCheck();
        val= GUILayout.HorizontalSlider(curvePoint.progress, 0, 1);
        curvePoint.StayAtSpline(val);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(curvePoint, "curvePoint");
            EditorUtility.SetDirty(curvePoint);
            curvePoint.progress = val;
        }
        
    }
}
