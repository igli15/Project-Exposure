using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFSM : EnemyFSM
{
    private Rigidbody m_rigidBody;
    private ArcherMovementState m_archerMovementState;

    public void Start()
    {
        base.Start();
    }

    public override void InitializeEnemy()
    {
        base.InitializeEnemy();
        m_archerMovementState = GetComponent<ArcherMovementState>();
        m_rigidBody = GetComponent<Rigidbody>();
        Debug.Log("Change State to movement");
        m_fsm.ChangeState<ArcherMovementState>();
    }

    public override void DestroyEnemy()
    {
        base.DestroyEnemy();
        
        m_archerMovementState.path = null;
        m_rigidBody.velocity = Vector3.zero;
        ObjectPooler.instance.DestroyFromPool("Archer", gameObject);
    }
}
