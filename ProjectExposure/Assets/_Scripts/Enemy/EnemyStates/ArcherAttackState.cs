using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArcherAttackState : AbstractState<EnemyFSM>
{
    public float waitingTime = 1;

    private float m_lastShotTime = 0;

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        m_lastShotTime = Time.time;
    }

    void Update()
    {
        if (Time.time - waitingTime > m_lastShotTime)
        {
            Shoot();
            m_lastShotTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<ArcherFSM>().DestroyEnemy();
        }
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
    }

    void Shoot()
    {
        transform.DOLookAt(Camera.main.transform.position, 0.5f);

        GameObject projectile = ObjectPooler.instance.SpawnFromPool("Projectile", transform.position, transform.rotation);
        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody>().velocity = (Camera.main.transform.position - transform.position).normalized * 10;
        projectile.GetComponent<Rigidbody>().useGravity = false;
    }
}
