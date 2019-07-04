using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvePoint : MonoBehaviour {

    [SerializeField]
    private BezierCurve m_spline;
    public float progress;
    public void StayAtSpline(float t)
    {
        if(m_spline!=null)
        transform.position=m_spline.GetPoint(t);
    }
}
