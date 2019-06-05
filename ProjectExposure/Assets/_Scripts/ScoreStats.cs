using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreStats : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField]
    public GameObject critBox;
    [SerializeField]
    public Text text;
    [SerializeField]
    public Text critText;

    [Header("General")]
    public static ScoreStats instance;
    [SerializeField]
    private float m_timeRange = 0.4f;

    private Tween m_critTween;

    private int m_currentBonus = 1;
    private int m_currentScore=0;
    private float m_lastTimeDeath = 0;
    private int m_tweenScore=0;
    private int m_tweenCritScore = 0;
    private int m_critScore=0;

    public bool isShot = false;

    public void Start()
    {
        critBox.transform.localScale = new Vector3(0, 1,1);
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
                else
                {
                    
                    critText.text = "";
                    HideCritBox();
                }
            }, 0, 0.2f);
        }
    }

    public void ShowCritBox()
    {
        m_critTween =critBox.transform.DOScaleX(1, 0.1f).OnComplete ( ()=> {  } ).SetEase(Ease.InQuad);
    }

    public void HideCritBox()
    {
        m_critTween=critBox.transform.DOScaleX(0, 0.1f).OnComplete(() => { }).SetEase(Ease.InQuad);
    }

    public void AddDeathData(Color color,Transform enemy,int currentBonus=1)
    {
        //If crit sequence is stoped
        if (Time.time - m_lastTimeDeath < m_timeRange)
        {
            if (m_currentBonus < 8)
                m_currentBonus++;
            
        }
        else
        {
            m_currentBonus = currentBonus;
        }
        ShowCritBox();
        isShot = true;

        int score = 0;
        string tag = "s100";
        Color currentColor = Color.white;
        switch (m_currentBonus) {
            case 1:
                score += 100;
                tag = "s100";
                currentColor = Color.white;
                break;
            case 2:
                score += 200;
                tag = "s200";
                currentColor = Color.blue;
                break;
            case 3:
                score += 400;
                tag = "s400";
                currentColor = Color.cyan;
                break;
            case 4:
                score += 800;
                tag = "s800";
                currentColor = Color.green;
                break;
            case 5:
                score += 1000;
                tag = "s1000";
                currentColor = Color.red;
                break;
            case 6:
                score += 2000;
                tag = "s2000";
                currentColor = Color.yellow;
                break;
            case 7:
                score += 4000;
                tag = "s4000";
                currentColor = Color.magenta;
                break;
            case 8:
                score += 8000;
                tag = "s8000";
                currentColor = Color.magenta;
                break;
        }

        if (m_currentBonus > 1)
        {
            
            m_tweenCritScore = m_critScore;
            m_critScore += score;

            critText.color = currentColor;
            DOTween.To(() => m_tweenCritScore, x =>
            {
                m_tweenCritScore = x;
                if (m_tweenCritScore != 0) critText.text = "" + m_tweenCritScore;
                else
                {
                    HideCritBox();
                    critText.text = "";
                }
            }, m_critScore, 0.2f);
           // critBox.SetActive(true);
        }
        else
        {
            m_critScore = score;
            
        }



        //Activating worldSpace scores
        GameObject scoreDisplay = ObjectPooler.instance.SpawnFromPool(tag, Camera.main.WorldToScreenPoint(enemy.position), new Quaternion(0, 0, 0, 0));
        scoreDisplay.transform.SetParent( transform.parent );
            //GameObject.Instantiate(bufferPrefab, Input.mousePosition, new Quaternion(0, 0, 0, 0), transform.parent);
        scoreDisplay.GetComponent<ScoreBehaviour>().ActivateScoreBehaviour(tag);
        m_lastTimeDeath = Time.time;
    }

    public void UpdateHighScore()
    {
        HighScoreManager.instance.highScore = m_currentScore;
    }
}
