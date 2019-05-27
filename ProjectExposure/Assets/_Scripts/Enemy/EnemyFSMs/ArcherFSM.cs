using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFSM : EnemyFSM
{
    private Rigidbody m_rigidBody;
    private ArcherMovementState m_archerMovementState;
    private Enemy m_enemy;
    public bool isIndependentAgent=false;
    [SerializeField]
    private float m_recoverTime = 0.8f;
    private bool m_isPushed=false;
    private float m_timeOfPush = 0;
    public void Start()
    {
        base.Start();
        m_enemy = GetComponent<Enemy>();
        m_enemy.OnPulled += OnPulled;
        m_enemy.OnPushed += OnPushed;
        m_enemy.OnReleased += OnReleased;
        GetComponent<Health>().OnDeath+=delegate(Health health){ DestroyEnemy(); };

        if (isIndependentAgent) InitializeEnemy();
    }

    public void OnPulled(Hittable hittable)
    {
        fsm.ChangeState<EnemyDisableState>();
        m_isPushed = false;
    }
    public void OnPushed(Hittable hittable)
    {
        m_isPushed = true;
        m_timeOfPush = Time.time;
    }
    public void OnReleased(Hittable hittable)
    {
        fsm.ChangeState<ArcherMovementState>();
        m_archerMovementState.GoToLastPoint();
    }

    public void Update()
    {
        if (m_isPushed && 
            m_timeOfPush + m_recoverTime < Time.time)
        {
            fsm.ChangeState<ArcherMovementState>();
            m_archerMovementState.GoToLastPoint();
            m_isPushed = false;
        }
    }

    public override void InitializeEnemy()
    {
        base.InitializeEnemy();
        
        m_archerMovementState = GetComponent<ArcherMovementState>();
        m_rigidBody = GetComponent<Rigidbody>();

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
