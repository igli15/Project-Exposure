using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour, IAgent {

    protected Fsm<EnemyFSM> m_fsm;
    public Fsm<EnemyFSM> fsm { get { return m_fsm; } }

    protected Health m_health;

	protected void Start () {
        if(m_fsm==null)
        m_fsm = new Fsm<EnemyFSM>(this);


    }


}
