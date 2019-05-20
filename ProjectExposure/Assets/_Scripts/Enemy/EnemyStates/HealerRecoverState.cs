using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerRecoverState : AbstractState<EnemyFSM> {
    [SerializeField]
    private float m_recoveringTime = 3;

    private float m_lastTimeRecover;
    private Enemy m_enemy;

    public void Start()
    {
        m_enemy = GetComponent<Enemy>();
    }

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        m_lastTimeRecover = Time.time;
        Debug.Log("RECOVER_STATE");
    }

    void Update()
    {
        if (Time.time > m_lastTimeRecover + m_recoveringTime)
        {
            Debug.Log("Swaping back to white color and going SwapCOlor");
            target.fsm.ChangeState<HealerSwapColorState>();
            return;
            target.fsm.ChangeState<HealerMovementState>();
            target.GetComponent<HealerMovementState>().GoToLastPoint();
        }
    }

    public override void Exit(IAgent pAgent)
    {
        m_enemy.SetColor(Color.white);
        base.Exit(pAgent);
    }

}
