using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyEscapeState : EnemyMovementState
{
    private float m_progress;

    public override void Enter(IAgent pAgent)
    {
        DOTween.To(() => m_progress, x => m_progress = x, 1, 3).SetEase(Ease.Linear).SetUpdate(true);
    }

    public override void Exit(IAgent pAgent)
    {
        
    }
}
