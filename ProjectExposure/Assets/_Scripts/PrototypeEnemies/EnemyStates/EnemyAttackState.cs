using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : AbstractState<EnemyFSM> {

    public GameObject prefab;
    public float waitingTime = 1;

    private float m_lastShotTime = 0;

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        Debug.Log("Attack ENTER");
        m_lastShotTime = Time.time;
    }

    void Update()
    {
        if (Time.time - waitingTime > m_lastShotTime)
        {
            Attack();
            m_lastShotTime = Time.time;
        }
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        Debug.Log("Attack EXIT");
    }

    void Attack()
    {
        Debug.Log("PEW PEW");
        return;
        GameObject projectile = GameObject.Instantiate(prefab,transform.position,transform.rotation,null);
        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody>().velocity =(  Camera.main.transform.position- transform.position ).normalized*2;
        projectile.GetComponent<Rigidbody>().useGravity = false;
    }
}
