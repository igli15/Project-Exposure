using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFSM : EnemyFSM
{
    private Rigidbody m_rigidBody;
    private ArcherMovementState m_archerMovementState;
    private Animator m_animator;
    [SerializeField]
    public int m_multiplierBonus = 2;
    private Enemy m_enemy;
    public bool isIndependentAgent=false;
    [SerializeField]
    private float m_recoverTime = 0.8f;
    private bool m_isPushed=false;
    private float m_timeOfPush = 0;

    public void Start()
    {
        base.Start();
        m_animator = GetComponent<Animator>();

        m_enemy = GetComponent<Enemy>();

        GetComponent<Health>().OnDeath+=delegate(Health health){ DestroyEnemy(); };

        if (isIndependentAgent) InitializeEnemy();
    }


    public void Update()
    {
        if (m_isPushed && 
            m_timeOfPush + m_recoverTime < Time.time)
        {
            fsm.ChangeState<ArcherMovementState>();
            //m_archerMovementState.GoToLastPoint();
            m_isPushed = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<Health>().InflictDamage(999);
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
        ScoreStats.instance.AddDeathData(m_enemy.GetColor(),2);

        m_rigidBody.velocity = Vector3.zero;
        ObjectPooler.instance.DestroyFromPool("Archer", gameObject);
    }
}
