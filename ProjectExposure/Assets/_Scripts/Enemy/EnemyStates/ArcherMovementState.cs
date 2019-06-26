using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ArcherMovementState : EnemyMovementState
{
    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CurvePoint"))
        target.fsm.ChangeState<ArcherAttackState>();
    }
}
