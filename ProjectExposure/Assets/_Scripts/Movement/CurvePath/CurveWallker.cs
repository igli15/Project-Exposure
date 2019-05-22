using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CurveWallker : MonoBehaviour
{
    public BezierCurve spline;

    public float duration;

    private float m_progress;
    public float progress
    {
        set
        {
            m_progress = value;
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
        get { return m_progress; }
    }
    public bool lookForward;
    public void Start()
    {
        DOTween.To(() => progress, x => progress = x, 1, duration).SetEase(Ease.Linear).SetUpdate(true);
    }
    private void Update()
    {
        return;
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
