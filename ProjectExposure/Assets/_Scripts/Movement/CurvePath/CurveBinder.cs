using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BezierCurve))]
public class CurveBinder : MonoBehaviour {

    [SerializeField]
    private BezierCurve m_spline;

    private BezierCurve m_bindedSpline;
    public float progress;

    public void Reset()
    {
        m_bindedSpline = GetComponent<BezierCurve>();
    }

    public void StayAtSpline(float t)
    {
        //if (m_spline != null) transform.position = m_spline.GetPoint(t);

        if (m_bindedSpline != null)
        {
            m_bindedSpline.SetRawPoint(0,m_spline.GetPoint(t)-transform.position);
            m_bindedSpline.SetRawPoint(1, m_spline.GetPoint(t) - transform.position + m_spline.GetDirection(t)*70 );
        }
    }
}
