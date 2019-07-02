using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossViewScript : MonoBehaviour
{

    public static BossViewScript instance;
    [SerializeField]
    GameObject m_mainFog;
    [SerializeField]
    GameObject m_expEffect;
    [SerializeField]
    GameObject m_riseEffect;
    [SerializeField]
    Transform m_eyeLip;
    [SerializeField]
    Gem m_mainGem;
    [SerializeField]
    List<TentacleBehaviour> m_tentacles;

    [SerializeField]
    List<GameObject> m_backgroundTentacle;

    [SerializeField]
    List<Transform> m_scoreTransforms;

    [SerializeField]
    GameObject m_crystal;
    [SerializeField]
    private Animator m_animator;
    Vector3 m_initialPos;
    private int m_tentacleCount = 0;
    private bool m_deafeated = false;
    public int directionOfTentacle = 1;
    public bool isActivated = false;

    int m_lifesAfterDeath = 1;

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

        m_tentacleCount = m_tentacles.Count;
        m_initialPos = transform.position;
        transform.position -= Vector3.up * 60;
    }

    public void ActivateBossFight()
    {
        if (isActivated) return;
        isActivated = true;

        GameObject.Instantiate(m_riseEffect, transform.position + transform.up * 10, m_riseEffect.transform.rotation, null);

        Camera.main.transform.DOShakePosition(3, 0.02f);
        transform.DOMoveY(m_initialPos.y, 4).OnComplete(ActivateNextTentacle);
    }

    public void ActivateNextTentacle()
    {
        if (m_tentacles.Count == 0)
        {
            return;
        }
        Debug.Log("Activate new");
        directionOfTentacle *= -1;
        m_tentacles[0].direction = directionOfTentacle;
        m_backgroundTentacle[0].transform.DOLocalMoveY(-20, 1.2f).OnComplete(
            () =>
            {
                m_tentacles[0].gameObject.SetActive(true);
                m_tentacles[0].enabled = true;
                m_tentacles[0].ActivateTentacle(m_crystal, transform.position.y - 20);

                m_tentacles.RemoveAt(0);
            }
        );
        m_backgroundTentacle.RemoveAt(0);


    }

    public void OpenBigEye()
    {
        m_animator.SetTrigger("death");
        m_mainGem.transform.DOScale(new Vector3(1, 1, 1) * 2, 1).OnComplete(() => {
            CurveWallker.instance.StopMovement();
            StartCoroutine(ActivateExplodesequence());
        });
        m_mainGem.onHit += (OnGemHit);
        m_eyeLip.transform.DOLocalRotate(new Vector3(-60, 0, 0), 2);

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
        Debug.Log("COUNT: " + m_tentacles.Count);

        if (m_tentacles.Count == 0)
        {
            OpenBigEye();
            return;
        }
        transform.DOLocalMoveY(transform.localPosition.y - 5, 2);
        ActivateNextTentacle();

    }

    public void BossDefeat()
    {

        Camera.main.transform.DOShakePosition(4, 0.02f).OnComplete(() => { GameObject.Instantiate(m_riseEffect, transform.position + transform.up * 10, m_riseEffect.transform.rotation, null); });
        transform.DOMoveY(m_initialPos.y - 70, 5).OnComplete(
            () =>
            {
                m_mainFog.transform.DOMoveY(m_mainFog.transform.position.y + 15, 2);
                m_mainFog.transform.DOScale(m_mainFog.transform.localScale * 2, 1);
            });
    }

    public void OnGemHit()
    {
        if (!m_deafeated)
        {

        }
        m_deafeated = true;
    }

    IEnumerator ActivateExplodesequence()
    {
        for (int i=0;i<7;i++)
        {
            Explode(5+i);
            yield return new WaitForSeconds(0.3f);
        }
        BossDefeat();
    }

    public void Explode(int numberOfScore)
    {
        Camera.main.transform.DOShakePosition(0.5f, 0.005f);
        for (int i = 0; i < numberOfScore; i++)
        {
            Transform randomTransform = m_scoreTransforms[UnityEngine.Random.Range(0, m_scoreTransforms.Count)];
            GameObject effect = GameObject.Instantiate(m_expEffect, randomTransform.position, transform.rotation, null);
            effect.transform.localScale = new Vector3(3, 3, 3);
            ScoreStats.instance.AddDeathData(Color.red, randomTransform, true);
        }
    }

    void Update()
    {
        if (CurveWallker.instance == null) CurveWallker.instance.lookForward = false;

        CurveWallker.instance.transform.LookAt(transform);

        transform.LookAt(CurveWallker.instance.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
