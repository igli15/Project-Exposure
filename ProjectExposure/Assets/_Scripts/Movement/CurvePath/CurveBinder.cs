using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BezierCurve))]
public class CurveBinder : MonoBehaviour {

    [SerializeField]
    private BezierCurve m_startSpline;
    [SerializeField]
    public float startProgress;

    [SerializeField]
    private BezierCurve m_endSpline;
    [SerializeField]
    public float endPprogress;

    public bool isActivated = false;

    private BezierCurve m_bindedSpline;



    public void Reset()
    {
        m_bindedSpline = GetComponent<BezierCurve>();
    }

    public void Update()
    {
        
        if (!CurveWallker.instance) return;

        if (Mathf.Abs(
            CurveWallker.instance.progress - startProgress)<0.001f &&
            isActivated&&
            CurveWallker.instance.spline==m_startSpline)
        {
            Debug.Log("MyProgress: " + startProgress + " PlayerProgress: " + CurveWallker.instance.progress);
            CurveWallker.instance.spline = GetComponent<BezierCurve>();
            CurveWallker.instance.progress = 0;
            isActivated = false;
        }
        
    }

    public void AttachStartNode(float t)
    {

        if (m_bindedSpline != null)
        {
            startProgress = t;
            m_bindedSpline.SetRawPoint(0, m_startSpline.GetPoint(t) - transform.position);
            m_bindedSpline.SetRawPoint(1, m_startSpline.GetPoint(t) - transform.position + m_startSpline.GetDirection(t) * 70);
        }
        else { m_bindedSpline = GetComponent<BezierCurve>(); }
    }

    public void AttachEndNode(float t)
    {
        if (m_bindedSpline != null)
        {
            endPprogress = t;
            m_bindedSpline.SetRawPoint(m_endSpline.ControlPointCount-1, m_endSpline.GetPoint(t) - transform.position);
            m_bindedSpline.SetRawPoint(m_endSpline.ControlPointCount-2, m_endSpline.GetPoint(t) - transform.position + m_endSpline.GetDirection(t) * 70);
        }
        else { m_bindedSpline = GetComponent<BezierCurve>(); }
    }
}
