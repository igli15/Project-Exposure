using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreStats : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField]
    public ComboMeter comboMeter;
    [SerializeField]
    public Text text;
    [SerializeField]
    public Text critText;

    [Header("General")]
    public static ScoreStats instance;
    [SerializeField]
    private float m_timeRange = 0.4f;
    public float degreePerInput=90;
    public float degreePenaltyPerSecond = 5;
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
        instance = this;
    }

    private void Update()
    {
        if (Time.time - m_lastTimeDeath > m_timeRange)
        {
            comboMeter.DecreaseFillImmediate(Time.timeScale * degreePenaltyPerSecond);
        }
    }


    public void AddDeathData(Color color,Transform enemy,bool isFish=false)
    {

        comboMeter.IncreaseFill(degreePerInput);
        isShot = true;

        //Default setting
        int score = 0;
        string tag = "s1";
        m_currentBonus = comboMeter.multiplier;
        Debug.Log("AddDeathFata");
        Color currentColor = Color.gray;

        if (isFish)
        {
            tag = "s" + (m_currentBonus*5);
            score = m_currentBonus*5;
        }
        else
        {
            tag = "s" + m_currentBonus;
            score = m_currentBonus;
        }

        switch (m_currentBonus) {
            case 1:
                //score += 100;
                //tag = "s1";
                currentColor = Color.gray;
                break;
            case 2:
                //score += 200;
                //tag = "s2";
                currentColor = Color.green;
                break;
            case 3:
                //score += 400;
                //tag = "s3";
                currentColor = Color.cyan;
                break;
            case 4:
                //score += 800;
                //tag = "s4";
                currentColor = Color.blue;
                break;
            case 5:
                //score += 1000;
                //tag = "s5";
                currentColor = new Color(80,50,20,1);
                break;
            case 6:
                //score += 2000;
                //tag = "s6";
                currentColor = new Color(192, 192, 192, 1);
                break;
            case 7:
               // score += 4000;
                //tag = "s7";
                currentColor = new Color(255, 215, 0, 1);
                break;
            case 8:
                //score += 8000;
                //tag = "s8";
                currentColor = new Color(185, 242, 255);
                break;
            case 9:
                //score += 8000;
                //tag = "s9";
                currentColor = new Color(185, 242, 255);
                break;
            case 10:
                //score += 8000;
                //tag = "s10";
                currentColor = new Color(185, 242, 255);
                break;
        }

        m_currentScore += score;

        DOTween.To(() => m_tweenScore, x => { m_tweenScore = x; text.text = "" + m_tweenScore; }, m_currentScore, 0.03f).SetEase(Ease.Linear).SetUpdate(true);

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
