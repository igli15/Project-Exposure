using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveWallker : MonoBehaviour {
    public BezierCurve spline;

    public float duration;

    private float m_progress;

    public bool lookForward;

    private void Update()
    {
        m_progress += Time.deltaTime / duration;
        if (m_progress > 1f)
        {
            m_progress = 1f;
        }
        Vector3 position = spline.GetPoint(m_progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(m_progress));
        }
    }
}
