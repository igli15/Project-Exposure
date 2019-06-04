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
    private bool m_isActive = true;
    private float m_durationMultiplier=1;
    public void Start()
    {
        if (isPlayer) instance = this;
       // DOTween.To(() => progress, x => progress = x, 1, duration).SetEase(Ease.Linear).SetUpdate(true);
    }
    private void Update()
    {
        if (!m_isActive || !spline) return;

        m_progress += Time.deltaTime / ( spline.Duration*m_durationMultiplier );
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

    public Vector3 GetPositionIn(float time)
    {
        Vector3 futurePosition = spline.GetPoint(m_progress+time/spline.Duration*m_durationMultiplier);
        return futurePosition;
    }

    public void StartMovement()
    {
        m_isActive = true;
    }

    public void StopMovement()
    {
        m_isActive = false;
    }

    public void SetDurationMultiplier(float value)
    {
        m_durationMultiplier = value;
    }

    public float GetDurationMultiplier()
    {
        return m_durationMultiplier;
    }
}
