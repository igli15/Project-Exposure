using System;
#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;


[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor
{
    private static Color[] m_modeColors = {
        Color.white,
        Color.yellow,
        Color.cyan
    };

    private BezierCurve m_curve;
    private Transform m_handleTransform;
    private Quaternion m_handleRotation;
    private bool m_drawInEditor=false;

    private const int m_lineSteps = 200;

    //Custom editor button
    private const float m_handleSize = 0.04f;
    private const float m_pickSize = 0.06f;
    private int m_selectedIndex = -1;

    void OnEnable()
    {
        //.onSceneGUIDelegate +=CustomOnSceneGUI;
    }

    void CustomOnSceneGUI(SceneView sceneview)
    {

        if (EditorApplication.isPlaying || EditorApplication.isPaused) return;
        if (null == target) return;
        if (null == m_curve) m_curve = target as BezierCurve;
        if ( !m_curve.enabled) return;

        Vector3 p0 = m_handleTransform.TransformPoint(m_curve.GetControlPoint((0)));
        for (int i = 1; i < m_curve.ControlPointCount; i += 3)
        {
            Vector3 p1 = m_handleTransform.TransformPoint(m_curve.GetControlPoint((i)));
            Vector3 p2 = m_handleTransform.TransformPoint(m_curve.GetControlPoint((i + 1)));
            Vector3 p3 = m_handleTransform.TransformPoint(m_curve.GetControlPoint((i + 2)));

            //Drawing actual bezierLine
            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
            p0 = p3;
        }
    }

    private void OnSceneGUI()
    {
        
        m_curve = target as BezierCurve;
        m_handleTransform = m_curve.transform;
        m_handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            m_handleTransform.rotation : Quaternion.identity;
        Vector3 p0 = ShowPoint(0);
        Handles.DrawLine(p0, new Vector3(p0.x, 0, p0.z));
        for (int i = 1; i < m_curve.ControlPointCount; i += 3)
        {
            Vector3 p1 = ShowPoint(i);
            Vector3 p2 = ShowPoint(i + 1);
            Vector3 p3 = ShowPoint(i + 2);

            //Drawing lines from points to constraints
            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p2, p3);


            Handles.DrawLine(p3, new Vector3(p3.x, 0, p3.z));
            //Drawing actual bezierLine
            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 4.5f);
            p0 = p3;
        }
        ShowHeights();
    }

    public override void OnInspectorGUI()
    {
        m_curve = target as BezierCurve;

        GUILayout.Label("DrawInEditor");
        bool bufferDrawInEditor = GUILayout.Toggle(m_drawInEditor, SplineManager.instance.TextureDisplay);

        if (bufferDrawInEditor != m_drawInEditor)
        {

        }

        m_drawInEditor = bufferDrawInEditor;
        Debug.Log("DrawInEditor: " + m_drawInEditor);

        EditorGUI.BeginChangeCheck();
        GUILayout.Label("Duration");
        float duration = EditorGUILayout.FloatField(m_curve.Duration);
        m_curve.Duration = duration;
        if (m_selectedIndex >= 0 && m_selectedIndex < m_curve.ControlPointCount)
        {
            DrawSelectedPointInspector();
        }
        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(m_curve, "Add Curve");
            m_curve.AddCurve();
            EditorUtility.SetDirty(m_curve);
        }
    }

    private void DrawSelectedPointInspector()
    {
        GUILayout.Label("Selected Point");
        EditorGUI.BeginChangeCheck();
        Vector3 point = EditorGUILayout.Vector3Field("Position", m_curve.GetControlPoint(m_selectedIndex));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_curve, "Move Point");
            EditorUtility.SetDirty(m_curve);
            m_curve.SetControlPoint(m_selectedIndex, point);
        }

        EditorGUI.BeginChangeCheck();
        BezierCurve.BezierControlPointMode mode = (BezierCurve.BezierControlPointMode)
            EditorGUILayout.EnumPopup("Mode", m_curve.GetControlPointMode(m_selectedIndex));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_curve, "Change Point Mode");
            m_curve.SetControlPointMode(m_selectedIndex, mode);
            EditorUtility.SetDirty(m_curve);
        }
    }

    private void ShowHeights()
    {
        Handles.color = Color.green;
        Vector3 point = m_curve.GetPoint(0f);
        Handles.DrawLine(point, point + m_curve.GetDirection(0f) );
        for (int i = 1; i <= m_lineSteps; i++)
        {
            point = m_curve.GetPoint(i / (float)m_lineSteps);
            Handles.color = (point.y > 0) ? Color.green : Color.red;
            Handles.DrawLine(point, new Vector3(point.x,0,point.z) );
        }
    }



    private Vector3 ShowPoint(int index)
    {
        Vector3 point = m_handleTransform.TransformPoint(m_curve.GetControlPoint(index));
        
        //Getting normal size based in Unity
        float size = HandleUtility.GetHandleSize(point);

        //Getting color based on point or constraint mode
        //Free     => White
        //Mirrored => Cyan
        //Alligned => Yellow
        Handles.color = m_modeColors[(int)m_curve.GetControlPointMode(index)];

        //If Button is pressed, update editor info and switch selected index to new
        if (Handles.Button(point, m_handleRotation, size * m_handleSize, size * m_pickSize, Handles.DotCap))
        {
            Repaint();
            m_selectedIndex = index;
        }

        //if we already selected this point or constraint then save all changes applied to its position
        if (m_selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();

            //Activate Unity-Move Handles
            point = Handles.DoPositionHandle(point, m_handleRotation);
            if (EditorGUI.EndChangeCheck())
            {

                Undo.RecordObject(m_curve, "Move Point");
                EditorUtility.SetDirty(m_curve);
                //updating data in spline
                m_curve.SetControlPoint(index, m_handleTransform.InverseTransformPoint(point));
                
            }
        }
        return point;
    }
}
#endif