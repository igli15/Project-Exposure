using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementState : AbstractState<EnemyFSM>
{
    public Path path;
    public float speed = 2;

    private Rigidbody m_rb;
    private MovementPoint m_currentTargetPoint;

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);

        if (!m_rb) m_rb = GetComponent<Rigidbody>();
        m_currentTargetPoint = path.GetFirstPoint();

        StartMovement();
    }

    private void Update()
    {

    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
    }

    public void StartMovement()
    {
        Vector3 direction = m_currentTargetPoint.transform.position - transform.position;
        m_rb.velocity = direction.normalized * speed;
    }

    public void StopMovement()
    {
        m_rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovementPoint")&&
            other.GetComponent<MovementPoint>().GetPath()==m_currentTargetPoint.GetPath())
        {
            
            if (other.GetComponent<MovementPoint>().GetNextPoint() == null)
            {
                //Stop movement on the end of path
                StopMovement();
                target.fsm.ChangeState<EnemyAttackState>();
                return;
            }
            MovementPoint bufferPoint = m_currentTargetPoint;

            //Move forward
            m_currentTargetPoint = other.GetComponent<MovementPoint>().GetNextPoint();
            StartMovement();

            //Activate all events binded to Point
            bufferPoint.ActivatePoint();
        }
    }
}
