using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossViewScript : MonoBehaviour {

    public static BossViewScript instance;

    [SerializeField]
    List<TentacleBehaviour> m_tentacles;

    [SerializeField]
    List<GameObject> m_backgroundTentacle;

    [SerializeField]
    GameObject m_crystal;
    [SerializeField]
    private Animator m_animator;

    private int m_tentacleCount=0;

    private void Start()
    {
        instance = this;
        //m_animator = GetComponent<Animator>();

        foreach (TentacleBehaviour tentacle in m_tentacles)
        {
            tentacle.onEnd += OnTentacleEnd;
            tentacle.Initialize();
            tentacle.boss = this;
            tentacle.enabled = false;
        }

        int i = 0;

        m_tentacleCount = m_tentacles.Count;
        transform.DOMoveY(transform.position.y, 1).OnComplete(ActivateNextTentacle);
    }

    public void ActivateNextTentacle()
    {
        if (m_tentacles.Count == 0) return;
        Debug.Log("Activate new");
        m_backgroundTentacle[0].transform.DOLocalMoveY(-20, 1.2f).OnComplete(
            ()=> {
            m_tentacles[0].gameObject.SetActive(true);
            m_tentacles[0].enabled = true;
            m_tentacles[0].ActivateTentacle(m_crystal, transform.position.y - 20);
            m_tentacles.RemoveAt(0);
            }
        );
        m_backgroundTentacle.RemoveAt(0);


    }

    public void TakeDamage()
    {
        m_animator.SetTrigger("takingDamage");
    }

    public void Attack()
    {
        m_animator.SetTrigger("attack");
    }

    public void SetDeath(bool death)
    {
        m_animator.SetBool("death", death);
    }

    public void OnTentacleEnd(TentacleBehaviour tentacle)
    {
        transform.DOLocalMoveY(transform.localPosition.y - 5, 2);
        ActivateNextTentacle();
    }

    void Update () {
        if(CurveWallker.instance==null) CurveWallker.instance.lookForward = false;

        CurveWallker.instance.transform.LookAt(transform);

        transform.LookAt(CurveWallker.instance.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
