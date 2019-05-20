using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor
{

    private BezierCurve m_curve;
    private Transform m_handleTransform;
    private Quaternion m_handleRotation;

    private const int lineSteps = 10;

    private void OnSceneGUI()
    {
        m_curve = target as BezierCurve;
        m_handleTransform = m_curve.transform;
        m_handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            m_handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);
        Vector3 p2 = ShowPoint(2);

        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p1, p2);

        Handles.color = Color.white;
        Vector3 lineStart = m_curve.GetPoint(0f);
        Handles.color = Color.green;
        Handles.DrawLine(lineStart, lineStart + m_curve.GetDirection(0f));
        for (int i = 1; i <= lineSteps; i++)
        {
            Vector3 lineEnd = m_curve.GetPoint(i / (float)lineSteps);
            Handles.color = Color.white;
            Handles.DrawLine(lineStart, lineEnd);
            Handles.color = Color.green;
            Handles.DrawLine(lineEnd, lineEnd + m_curve.GetDirection(i / (float)lineSteps));
            lineStart = lineEnd;
        }



    }

    private Vector3 ShowPoint(int index)
    {
        Vector3 point = m_handleTransform.TransformPoint(m_curve.points[index]);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, m_handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_curve, "Move Point");
            EditorUtility.SetDirty(m_curve);
            m_curve.points[index] = m_handleTransform.InverseTransformPoint(point);
        }
        return point;
    }
}