using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerSwapColorState : AbstractState<EnemyFSM> {

    [SerializeField]
    private float m_chargningTime = 1;

    private float m_lastTimeCharge;

    EnemyFSM m_targetEnemy;
    Enemy m_enemy;

    private void Start()
    {
        m_enemy = GetComponent<Enemy>();
    }

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        m_targetEnemy = GetRandomEnemyInRange(100);
        m_lastTimeCharge = Time.time;
        Debug.Log("ENEMY: " + m_targetEnemy.gameObject);
    }

    void Update()
    {
        if (Time.time > m_lastTimeCharge + m_chargningTime)
        {
            target.fsm.ChangeState<HealerRecoverState>();
            SwapColorsWith(m_targetEnemy.GetComponent<Enemy>());
        }
    }

    void SwapColorsWith(Enemy targetEnemy)
    {
        m_enemy.SetColor(targetEnemy.GetColor());
        targetEnemy.SetColor(Color.white);
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
    }

    EnemyFSM GetRandomEnemyInRange(float range)
    {
        Collider[] colliders=Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            if (collider.GetComponent<EnemyFSM>()) return collider.GetComponent<EnemyFSM>();
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        return; //for now
        if (this.enabled)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, m_targetEnemy.transform.position);
        }
    }
}
