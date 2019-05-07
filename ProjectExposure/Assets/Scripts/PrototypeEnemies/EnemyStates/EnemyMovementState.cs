using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementState : AbstractState<EnemyFSM>
{
    public Vector3 m_targetPosition;
    public float speed = 0.1f;

    private Vector3 m_direction;

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);

        m_direction = m_targetPosition-transform.position;
        m_direction = m_direction.normalized;

        //Debug.Log("Movement Enter");
    }

    private void Update()
    {
        transform.position += m_direction*speed;

        if ( (transform.position - m_targetPosition).magnitude <= speed)
        {
            target.fsm.ChangeState<EnemyAttackState>();
        }
    }

    public override void Exit(IAgent pAgent)
    {
        //Debug.Log("Movement Exit");
        base.Exit(pAgent);
    }
}
