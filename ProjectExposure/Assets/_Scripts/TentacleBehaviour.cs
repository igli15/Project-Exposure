using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TentacleBehaviour : MonoBehaviour {

    [SerializeField]
    [Range(0,1)]
    public float range = 0.1f;

    [SerializeField]
    float m_animationTime=5;
    [SerializeField]
    float m_vulnerableTime = 5;
    [SerializeField]
    float m_tweenTransferTime = 5;
    [SerializeField]
    Animator m_animator;

    [SerializeField]
    public List<Transform> placeholders;

    public BossViewScript boss;

    int m_crystalCount;
    int m_currentCount;
    float m_activationTime;
    bool m_inAnimationProgress = false;
    public Action<TentacleBehaviour> onEnd;

	public void Start () {

    }

    public void Initialize()
    {
        m_crystalCount = placeholders.Count;
        m_currentCount = m_crystalCount;

    }

    void Update () {

        if (!m_inAnimationProgress && m_activationTime + m_vulnerableTime < Time.time)
        {
            Attack();
        }

        Vector3 distance = ( BossViewScript.instance.transform.position- CurveWallker.instance.transform.position );
        distance *= range;
        Vector3 buffer = CurveWallker.instance.transform.position + distance;
        buffer.y = transform.position.y;
        transform.position = buffer;
        transform.LookAt(CurveWallker.instance.transform);
    }

    void Attack()
    {
        m_animator.SetTrigger("attack");
        
        m_inAnimationProgress = true;

        transform.DOLocalMoveX(0, 1.5f).OnComplete(boss.Attack); //after delay of 1 sec 

        transform.DOLocalMoveY(-60, 2).SetDelay(m_animationTime).OnComplete(
            () =>{
                m_animator.SetTrigger("back");
                transform.DOLocalMoveY(6, m_tweenTransferTime);
                m_activationTime = Time.time + m_tweenTransferTime;
                m_inAnimationProgress = false;
            }
        );
    }

    void OnCrystalExplode(Crystal c)
    {
       

        m_currentCount--;
        if (m_currentCount <= 0)
        {
            boss.TakeDamage();
            HideTentacle();
        }
    }

    public void ActivateTentacle(GameObject crystal)
    {
        m_activationTime = Time.time;

        foreach (Transform t in placeholders)
        {
            GameObject newCrytsal = GameObject.Instantiate(crystal, t.position, t.rotation, t);
            Crystal c = newCrytsal.GetComponent<Crystal>();
            c.OnExplode += OnCrystalExplode;

            /*c.SetColor(new Color(
                UnityEngine.Random.Range(0, 255)/255.0f, UnityEngine.Random.Range(0, 255) / 255.0f,
                UnityEngine.Random.Range(0, 255) / 255.0f, 1));*/
            c.SetColor(new Color(1,0,0, 1));


        }
        transform.position = new Vector3(transform.position.x, -60, transform.position.z);
        transform.DOMoveY(6, m_tweenTransferTime);
    }

    public void HideTentacle()
    {
        enabled = false;
        transform.DOLocalMoveY(-60, 1).OnComplete(() => { gameObject.SetActive(false); if (onEnd != null) onEnd(this); });
    }
}
