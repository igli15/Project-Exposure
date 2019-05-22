using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyMovementState : AbstractState<EnemyFSM>
{
    //Legacy
    public Path path;
    public float speed = 2;
    private Rigidbody m_rb;

    //Current
    public BezierCurve spline;
    public bool lookForward;
    public float duration;
    private float m_progress;

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        if (!m_rb) m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
        
        
    }

    private void Update()
    {
        m_progress += Time.deltaTime / duration;
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
        m_rb.velocity = Vector3.zero;
        m_rb.useGravity = false;
    }

    public void GoToLastPoint()
    {

    }

    public void StartMovement()
    {

    }

    public void StopMovement()
    {

    }

    public virtual void OnLastPointActivated()
    {
        //Stop movement on the end of path

    }

    public virtual void OnPointEntered()
    {
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
