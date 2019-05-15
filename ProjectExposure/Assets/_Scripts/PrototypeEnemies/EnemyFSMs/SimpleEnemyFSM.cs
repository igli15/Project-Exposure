using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyFSM : EnemyFSM, IPooleableObject {

    
    private Rigidbody m_rigidBody;
    private EnemyMovementState m_enemyMovementState;
    public void OnObjectSpawn()
    {
        
    }

    private void Start () {
        //ERROR HERE
        base.Start();
        //Initialize();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyEnemy();
        }
	}

    public override void InitializeEnemy()
    {
        Debug.Log("Initialize");
        m_enemyMovementState = GetComponent<EnemyMovementState>();
        m_rigidBody = GetComponent<Rigidbody>();
        if (m_fsm == null)
            m_fsm = new Fsm<EnemyFSM>(this);
        m_fsm.ChangeState<EnemyMovementState>();
    }

    public override void DestroyEnemy()
    {
        Debug.Log("OnEnemyDestroy");
        if (onDeath != null) onDeath(this);

        m_enemyMovementState.path = null;
        m_rigidBody.velocity = Vector3.zero;
        ObjectPooler.instance.DestroyFromPool("SimpleEnemy", gameObject);
    }
}
