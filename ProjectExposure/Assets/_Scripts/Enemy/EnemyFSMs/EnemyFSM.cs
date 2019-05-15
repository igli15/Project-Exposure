using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour, IAgent {

    public Action<EnemyFSM> onDeath;
    protected Fsm<EnemyFSM> m_fsm;
    public Fsm<EnemyFSM> fsm { get { return m_fsm; } }

	protected void Start () {
        if(m_fsm==null)
        m_fsm = new Fsm<EnemyFSM>(this);
    }

    public virtual void InitializeEnemy()
    {
        if (m_fsm == null)
            m_fsm = new Fsm<EnemyFSM>(this);
    }

    public virtual void DestroyEnemy()
    {
        if (onDeath != null) onDeath(this);
    }

}
