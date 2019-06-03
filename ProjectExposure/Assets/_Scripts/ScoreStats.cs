using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreStats : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField]
    public Text text;
    [SerializeField]
    public Text critText;

    [Header("ScoreNumber")]
    [SerializeField]
    public GameObject prefab100;
    [SerializeField]
    public GameObject prefab200;
    [SerializeField]
    public GameObject prefab400;
    [SerializeField]
    public GameObject prefab800;
    [SerializeField]
    public GameObject prefab1000;
    [SerializeField]
    public GameObject prefab2000;
    [SerializeField]
    public GameObject prefab4000;
    [SerializeField]
    public GameObject prefab8000;

    [Header("General")]
    public static ScoreStats instance;
    [SerializeField]
    private float m_timeRange = 0.4f;

    private int m_currentBonus = 1;
    private int m_currentScore=0;
    private float m_lastTimeDeath = 0;
    private int m_tweenScore=0;
    private int m_critScore=0;

    public bool isShot = false;

    public void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (isShot&& Time.time - m_lastTimeDeath > m_timeRange)
        {
            isShot = false;
            m_currentScore += m_critScore;
            
            DOTween.To(() => m_tweenScore, x => { m_tweenScore = x; text.text = "" + m_tweenScore; }, m_currentScore, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
            DOTween.To(() => m_critScore, x => 
            {
                m_critScore = x;
                if (m_critScore != 0) critText.text = "" + m_critScore;
                else critText.text = "";
            }, 0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);

           // m_critScore = 0;
            //critText.text = "" ;
        }
    }

    

    public void AddDeathData(Color color,Transform enemy,int currentBonus=1)
    {

        if (Time.time - m_lastTimeDeath < m_timeRange)
        {
            if (m_currentBonus < 8)
                m_currentBonus++;
        }
        else
        {
            m_currentBonus = currentBonus;
        }

        isShot = true;

        int score = 0;
        string tag = "s100";
        GameObject bufferPrefab=null;
        switch (m_currentBonus) {
            case 1:
                score += 100;
                bufferPrefab = prefab100;
                tag = "s100";
                break;
            case 2:
                score += 200;
                bufferPrefab = prefab200;
                tag = "s200";
                break;
            case 3:
                score += 400;
                bufferPrefab = prefab400;
                tag = "s400";
                break;
            case 4:
                score += 800;
                bufferPrefab = prefab800;
                tag = "s800";
                break;
            case 5:
                score += 1000;
                bufferPrefab = prefab1000;
                tag = "s1000";
                break;
            case 6:
                score += 2000;
                bufferPrefab = prefab2000;
                tag = "s2000";
                break;
            case 7:
                score += 4000;
                bufferPrefab = prefab4000;
                tag = "s4000";
                break;
            case 8:
                score += 8000;
                bufferPrefab = prefab8000;
                tag = "s8000";
                break;
        }

        if (m_currentBonus > 1)
        {
            m_critScore += score;
            critText.text = "" + m_critScore;
        }
        else
        {
            m_critScore = score;
            
        }




        GameObject scoreDisplay = ObjectPooler.instance.SpawnFromPool(tag, Camera.main.WorldToScreenPoint(enemy.position), new Quaternion(0, 0, 0, 0));
        scoreDisplay.transform.parent = transform.parent;
            //GameObject.Instantiate(bufferPrefab, Input.mousePosition, new Quaternion(0, 0, 0, 0), transform.parent);
        scoreDisplay.GetComponent<ScoreBehaviour>().ActivateScoreBehaviour(tag);
        m_lastTimeDeath = Time.time;
    }
}
