using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CurveWallker : MonoBehaviour
{
    [SerializeField]
    public BezierCurve spline;
    [SerializeField]
    public bool isPlayer=false;

    public static CurveWallker instance;

    public float duration;
    public float speed = 2;
    private float m_progress;
    public float progress
    {
        set
        {
            m_progress = value;
        }
        get { return m_progress; }
    }
    public bool lookForward;
    public void Start()
    {
        if (isPlayer) instance = this;
       // DOTween.To(() => progress, x => progress = x, 1, duration).SetEase(Ease.Linear).SetUpdate(true);
    }
    private void Update()
    {
        
        m_progress += Time.deltaTime / duration;
        if (m_progress > 1f)
        {
            m_progress = 1f;
            if (spline.GetComponent<CurveBinder>())
            {
                CurveBinder binder = spline.GetComponent<CurveBinder>();
                if (binder.EndSpline)
                {
                    spline = binder.EndSpline;
                    progress = binder.endPprogress;
                }
            }
        }
        Vector3 position = spline.GetPoint(m_progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(m_progress));
        }
    }
}
