﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BezierCurve))]
public class CurveBinder : MonoBehaviour {

    [SerializeField]
    private BezierCurve m_startSpline;
    [SerializeField]
    public float startProgress;
    [SerializeField]
    public float startConstrainStrength = 30;
    [SerializeField]
    private BezierCurve m_endSpline;
    [SerializeField]
    public float endPprogress;
    [SerializeField]
    public float endConstrainStrength = 30;

    //TODO: scale of constrain for merging parts

    public bool isActivated = false;

    private BezierCurve m_bindedSpline;

    public BezierCurve EndSpline { get { return m_endSpline; } }
    public BezierCurve StartSpline { get { return m_startSpline; } }

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
            //Debug.Log("MyProgress: " + startProgress + " PlayerProgress: " + CurveWallker.instance.progress);
            CurveWallker.instance.spline = GetComponent<BezierCurve>();
            CurveWallker.instance.progress = 0;
            isActivated = false;
        }
        
    }

    public void AttachStartNode(float t)
    {
        if (!m_bindedSpline) { m_bindedSpline = GetComponent<BezierCurve>(); }
        if (m_bindedSpline && m_startSpline)
        {
            startProgress = t;
            m_bindedSpline.SetRawPoint(0, m_startSpline.GetPoint(t) - transform.position);
            m_bindedSpline.SetRawPoint(1, m_startSpline.GetPoint(t) - transform.position + m_startSpline.GetDirection(t) * startConstrainStrength);
        }
    }

    public void AttachEndNode(float t)
    {
        if(!m_bindedSpline) { m_bindedSpline = GetComponent<BezierCurve>(); }
        if (m_bindedSpline && m_endSpline)
        {
            endPprogress = t;
            //fix last point getter
            m_bindedSpline.SetRawPoint(m_bindedSpline.ControlPointCount-1, m_endSpline.GetPoint(t) - transform.position);
            m_bindedSpline.SetRawPoint(m_bindedSpline.ControlPointCount-2, m_endSpline.GetPoint(t) - transform.position - m_endSpline.GetDirection(t) * endConstrainStrength);

        }
        
    }
}
