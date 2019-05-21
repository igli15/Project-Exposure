using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
        m_rb.useGravity = false;
        if(m_currentTargetPoint==null)
        m_currentTargetPoint = path.GetFirstPoint();
        
        StartMovement();
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        m_rb.velocity = Vector3.zero;
        m_rb.useGravity = false;
    }

    public void GoToLastPoint()
    {
        m_currentTargetPoint = path.GetLastPoint();
        StartMovement();
    }

    public void StartMovement()
    {
        transform.DOLookAt(m_currentTargetPoint.transform.position, 0.5f);
        Vector3 direction = m_currentTargetPoint.transform.position - transform.position;
        m_rb.velocity = direction.normalized * speed;
    }

    public void StopMovement()
    {
        m_rb.velocity = Vector3.zero;
    }

    public virtual void OnLastPointActivated()
    {
        //Stop movement on the end of path

    }

    public virtual void OnPointEntered()
    {
        StartMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovementPoint")&&
            other.GetComponent<MovementPoint>().GetPath()==m_currentTargetPoint.GetPath())
        {
            
            if (other.GetComponent<MovementPoint>().GetNextPoint() == null)
            {
                OnLastPointActivated();
                return;
            }
            MovementPoint bufferPoint = m_currentTargetPoint;

            //Move forward
            m_currentTargetPoint = other.GetComponent<MovementPoint>().GetNextPoint();
            OnPointEntered();

            //Activate all events binded to Point
            bufferPoint.ActivatePoint();
        }
    }
}
