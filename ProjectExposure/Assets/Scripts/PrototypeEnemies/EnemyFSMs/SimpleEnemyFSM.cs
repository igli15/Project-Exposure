using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyFSM : EnemyFSM {

    // Use this for initialization
    private void Start () {
        base.Start();
        m_fsm.ChangeState<EnemyMovementState>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
