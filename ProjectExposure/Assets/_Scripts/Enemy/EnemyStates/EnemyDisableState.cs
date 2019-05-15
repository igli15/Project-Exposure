using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisableState : AbstractState<EnemyFSM> {
    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        
    }

    void Update()
    {

    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
    }
}
