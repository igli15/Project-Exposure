using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BezierCurve))]
public class CurveBinder : MonoBehaviour {

    [SerializeField]
    private BezierCurve m_spline;
    public bool isActivated = false;

    private BezierCurve m_bindedSpline;
    [SerializeField]
    public float progress;

    public void Reset()
    {
        m_bindedSpline = GetComponent<BezierCurve>();
    }

    public void Update()
    {
        
        if (!CurveWallker.instance) return;

        if (Mathf.Abs(
            CurveWallker.instance.progress - progress)<0.001f &&
            isActivated&&
            CurveWallker.instance.spline==m_spline)
        {
            Debug.Log("MyProgress: " + progress + " PlayerProgress: " + CurveWallker.instance.progress);
            CurveWallker.instance.spline = GetComponent<BezierCurve>();
            CurveWallker.instance.progress = 0;
            isActivated = false;
        }
        
    }

    public void StayAtSpline(float t)
    {

        if (m_bindedSpline != null)
        {
            progress = t;
            m_bindedSpline.SetRawPoint(0, m_spline.GetPoint(t) - transform.position);
            m_bindedSpline.SetRawPoint(1, m_spline.GetPoint(t) - transform.position + m_spline.GetDirection(t) * 70);
        }
        else { m_bindedSpline = GetComponent<BezierCurve>(); }
    }
}
