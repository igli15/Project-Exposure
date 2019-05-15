using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void OnLastPointActivated()
    {
        StopMovement();
        target.fsm.ChangeState<ArcherAttackState>();
    }
}
