using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementState : AbstractState<EnemyFSM>
{
    public Path path;
    public float speed = 0.1f;

    private Rigidbody m_rb;
    private MovementPoint m_currentTargetPoint;

    public override void Enter(IAgent pAgent)
    {
        Debug.Log("Movement Enter");
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
        Debug.Log("Movement Exit");
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
        if (other.CompareTag("MovementPoint"))
        {
            if (other.GetComponent<MovementPoint>().GetNextPoint() == null)
            {
                //Stop movement on the end of path
                StopMovement();
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
