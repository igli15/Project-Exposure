using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerFSM : EnemyFSM {
    private Rigidbody m_rigidBody;
    private HealerMovementState m_healerMovementState;
    private Enemy m_enemy;

    [SerializeField]
    private float m_recoverTime = 0.8f;
    private bool m_isPushed = false;
    private float m_timeOfPush = 0;

    public void Start()
    {
        base.Start();
        m_enemy = GetComponent<Enemy>();
        m_enemy.OnPulled += OnPulled;
        m_enemy.OnPushed += OnPushed;
        m_enemy.OnReleased += OnReleased;
        GetComponent<Health>().OnDeath += delegate (Health health) { DestroyEnemy(); };
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
        fsm.ChangeState<HealerMovementState>();
        m_healerMovementState.GoToLastPoint();
    }

    public void Update()
    {
        if (m_isPushed &&
            m_timeOfPush + m_recoverTime < Time.time)
        {
            fsm.ChangeState<HealerMovementState>();
            m_healerMovementState.GoToLastPoint();
            m_isPushed = false;
        }
    }

    public override void InitializeEnemy()
    {
        base.InitializeEnemy();

        m_healerMovementState = GetComponent<HealerMovementState>();
        m_rigidBody = GetComponent<Rigidbody>();

        Debug.Log("Change State to movement");
        m_fsm.ChangeState<HealerMovementState>();
    }

    public override void DestroyEnemy()
    {
        base.DestroyEnemy();

        m_healerMovementState.path = null;
        m_rigidBody.velocity = Vector3.zero;
        ObjectPooler.instance.DestroyFromPool("Healer", gameObject);
    }
}
