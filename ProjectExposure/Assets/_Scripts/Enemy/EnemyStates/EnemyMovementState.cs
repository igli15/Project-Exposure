using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyMovementState : AbstractState<EnemyFSM>
{
    //Current
    public BezierCurve spline;
    public bool lookForward;

    public bool TweenMovement = false;

    private float m_progress;

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);

    }

    private void Update()
    {
        if (TweenMovement) return;

        if (!spline)
        {
            Debug.Log("Attach spline to an Enemy: " + gameObject.name);
            return;
        }
        m_progress += Time.deltaTime / spline.Duration;
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

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
    }

    public void GoToTweenMovement(BezierCurve Spline)
    {
        TweenMovement = true;
        spline = Spline;
        m_progress = 0;



        DOTween.To(() => m_progress, x =>
        {
            m_progress = x;
            transform.position = spline.GetPoint(m_progress);
            transform.LookAt(transform.position + spline.GetDirection(m_progress));
        }
        , 1, Spline.Duration).SetEase(Ease.Linear).SetUpdate(true);


        
    }
}
