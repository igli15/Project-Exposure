using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArcherFSM : EnemyFSM
{
    private Rigidbody m_rigidBody;
    private ArcherMovementState m_archerMovementState;
    private Animator m_animator;

    [SerializeField]
    private BezierCurve m_escapeSpline;

    [SerializeField]
    public int m_multiplierBonus = 2;
    private Enemy m_enemy;
    public bool isIndependentAgent=false;
    [SerializeField]
    private float m_recoverTime = 0.8f;
    private bool m_isPushed=false;
    private float m_timeOfPush = 0;

    private bool m_isDead = false;

    public void Start()
    {
        base.Start();
        m_animator = GetComponent<Animator>();

        m_enemy = GetComponent<Enemy>();
        m_enemy.OnHit.AddListener( () => {
            GameObject explotion= ObjectPooler.instance.SpawnFromPool("fishPlotion", transform.position, transform.rotation);
            explotion.SetActive(false);
            explotion.SetActive(true);
        } 
        ); 
        GetComponent<Health>().OnDeath+=(Health health)=>{ DestroyEnemy(); };

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
            //GetComponent<Health>().InflictDamage(999);
        }
    }

    public override void InitializeEnemy()
    {
        //if (!m_isDead) return;
        base.InitializeEnemy();
        
        m_archerMovementState = GetComponent<ArcherMovementState>();
        m_archerMovementState.TweenMovement = false;
        m_rigidBody = GetComponent<Rigidbody>();

        m_fsm.ChangeState<ArcherMovementState>();
        m_escapeSpline.transform.SetParent(transform);

        m_isDead = false;
    }

    public override void DestroyEnemy()
    {
        if (m_isDead) return;
        ScoreStats.instance.AddDeathData(m_enemy.GetColor(),transform,2);
        
        base.DestroyEnemy();

        //fsm.ChangeState<ArcherMovementState>();
        m_escapeSpline.transform.parent = null;
        GetComponent<ArcherMovementState>().GoToTweenMovement(m_escapeSpline);

        m_rigidBody.velocity = Vector3.zero;

        m_isDead = true;
    }
}
