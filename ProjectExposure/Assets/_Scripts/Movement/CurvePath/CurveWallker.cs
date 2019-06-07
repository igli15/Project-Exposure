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
    private float m_tweenProgress;

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

        SplineMove(m_progress);
    }

    void SplineMove(float p)
    {
        if (p > 1f)
        {
            p = 1f;
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
        Vector3 position = spline.GetPoint(p);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(p));
        }
    }


    public Vector3 GetPositionIn(float time)
    {
        Vector3 futurePosition = spline.GetPoint(m_progress+time/spline.Duration*m_durationMultiplier);
        return futurePosition;
    }

    public Vector3 GetDirectionIn(float time)
    {
        Vector3 futureDirection = spline.GetDirection(m_progress + time / spline.Duration * m_durationMultiplier);
        return futureDirection;
    }

    public void StartMovement()
    {
        m_tweenProgress = 0;
        DOTween.To(() => m_tweenProgress, x => { m_tweenProgress = x; m_progress += x; SplineMove(m_progress);},
        Time.deltaTime / (spline.Duration * m_durationMultiplier), spline.Duration * m_durationMultiplier*0.05f).SetEase(Ease.InQuad).onComplete += () => { m_isActive = true;  };
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
