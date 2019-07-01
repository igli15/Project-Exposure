using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TentacleBehaviour : MonoBehaviour {

    [SerializeField]
    GameObject m_riseTentacleEffect;
    [SerializeField]
    [Range(0,1)]
    public float range = 0.1f;
    [SerializeField]
    float m_riseAnimTime = 2;
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
    public int direction = 1;

    int m_crystalCount;
    int m_currentCount;
    float m_activationTime;
    bool m_inAnimationProgress = false;
    public Action<TentacleBehaviour> onEnd;

    private Color[] m_colors = new Color[]{
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.cyan,
        new Color(1,0.5f,0,1), //or4ange
        new Color(0.5f,0,0.5f,1)
    };

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
        distance -= new Vector3(0, distance.y, 0);
        distance *= range;
        Vector3 buffer = CurveWallker.instance.transform.position + distance;
        buffer.y = transform.position.y;
        transform.position = buffer-transform.right*direction*10;
        transform.LookAt(CurveWallker.instance.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void Attack()
    {
        
        m_animator.SetTrigger("attack");
        
        m_inAnimationProgress = true;

        transform.DOLocalMoveX(0, 1.5f).OnComplete(()=> { boss.Attack();  }); //after delay of 1 sec 

        transform.DOLocalMoveY(-60, 2).SetDelay(m_animationTime).OnComplete(
            () =>{
                m_animator.SetTrigger("back");
                direction *= -1;

                Camera.main.transform.DOShakePosition(1, 0.02f);
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

    public void SpawnFogEffect()
    {
        //fartEffect.transform.LookAt(Camera.main.transform);
    }

    public void ActivateTentacle(GameObject crystal,float ypos)
    {
        m_activationTime = Time.time;
        Camera.main.transform.DOShakePosition(1, 0.02f).SetDelay(0.5f);
        foreach (Transform t in placeholders)
        {
            GameObject newCrytsal = GameObject.Instantiate(crystal, t.position, t.rotation, t);
            Crystal c = newCrytsal.GetComponent<Crystal>();
            c.OnExplode += OnCrystalExplode;

            /*c.SetColor(new Color(
                UnityEngine.Random.Range(0, 255)/255.0f, UnityEngine.Random.Range(0, 255) / 255.0f,
                UnityEngine.Random.Range(0, 255) / 255.0f, 1));*/
            c.SetColor(m_colors[UnityEngine.Random.Range(0, m_colors.Length-1)]);


        }
        transform.position = new Vector3(transform.position.x, -60, transform.position.z);
        transform.DOMoveY(ypos, m_riseAnimTime);
        
        GameObject fartEffect = GameObject.Instantiate(m_riseTentacleEffect, transform.position - transform.right * direction * 10 + new Vector3(0, transform.position.y + 100, 0), m_riseTentacleEffect.transform.rotation, null);

        SpawnFogEffect();
    }

    public void HideTentacle()
    {
        enabled = false;
        transform.DOLocalMoveY(-60, 1).OnComplete(() => { gameObject.SetActive(false); if (onEnd != null) onEnd(this); });
    }
}
