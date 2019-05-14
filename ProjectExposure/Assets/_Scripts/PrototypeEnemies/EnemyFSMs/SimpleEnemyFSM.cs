using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyFSM : EnemyFSM, IPooleableObject {

    public Action<SimpleEnemyFSM> onDeath;

    private EnemyMovementState m_enemyMovementState;
    public void OnObjectSpawn()
    {
        //Reset all varables here
        //m_enemyMovementState.path = null;
    }

    // Use this for initialization
    private void Start () {
        base.Start();
        m_fsm.ChangeState<EnemyMovementState>();
        m_enemyMovementState = GetComponent<EnemyMovementState>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DestroyEnemy();
        }
	}

    public void DestroyEnemy()
    {
        Debug.Log("OnEnemyDestroy");
        if (onDeath != null) onDeath(this);

        m_enemyMovementState.path = null;
        ObjectPooler.instance.DestroyFromPool("SimpleEnemy", gameObject);
    }
}
