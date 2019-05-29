using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArcherAttackState : AbstractState<EnemyFSM>
{
    public float waitingTime = 1;
    public float waitingTimeAfter = 1;
    public float rangeOfShooting = 300;
    [SerializeField]
    private float m_projectileSpeed = 10;
    private float m_lastShotTime = 0;
    [SerializeField]
    private float m_lookAtDuration = 0.5f;
    
    private bool m_isFreezed = false; 
    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        m_lastShotTime = Time.time;
        transform.DOLookAt(Camera.main.transform.position, m_lookAtDuration);
    }

    void Update()
    {
        if (Time.time - (m_isFreezed ? waitingTimeAfter:waitingTime)
            >m_lastShotTime&& IsPlayerInRange(rangeOfShooting))
        {
            if (m_isFreezed == false)
            {
                Debug.Log("shoot!");
                Shoot();
                m_isFreezed = true;
            }
            else
            {
                target.fsm.ChangeState<ArcherMovementState>();
            }
            m_lastShotTime = Time.time;
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<ArcherFSM>().DestroyEnemy();
        }
    }

    public override void Exit(IAgent pAgent)
    {
        m_isFreezed = false;
        base.Exit(pAgent);
    }

    void Shoot()
    {
        Debug.Log("iam shootimg");
        transform.DOLookAt(Camera.main.transform.position, 0.5f);
        GetComponent<Animator>().SetTrigger("shoot");
        GameObject projectile = ObjectPooler.instance.SpawnFromPool("Projectile", transform.position, transform.rotation);
        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody>().velocity = (Camera.main.transform.position - transform.position).normalized * m_projectileSpeed;
        projectile.GetComponent<Rigidbody>().useGravity = false;

        //target.fsm.ChangeState<ArcherMovementState>();
    }
    bool IsPlayerInRange(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}
