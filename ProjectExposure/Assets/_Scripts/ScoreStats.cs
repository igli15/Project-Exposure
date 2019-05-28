using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreStats : MonoBehaviour
{
    [SerializeField]
    public Text text;

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

    public static ScoreStats instance;

    private float m_timeRange = 0.4f;

    private int m_currentBonus = 1;
    private int m_currentScore=0;
    private float m_lastTimeDeath = 0;
    public void Start()
    {
        instance = this;
    }

    private void Update()
    {
        return;
        //Comment return to test score stats
        if (Input.GetMouseButtonDown(0))
        {
            AddDeathData(Color.red, transform);
        }
    }

    public void AddDeathData(Color color, Transform transform)
    {

        if (Time.time - m_lastTimeDeath < m_timeRange)
        {
            if(m_currentBonus<8)
                m_currentBonus++;
        }
        else m_currentBonus = 1;

        int score = 0;

        GameObject bufferPrefab=null;
        switch (m_currentBonus) {
            case 1:
                score += 100;
                bufferPrefab = prefab100;
                break;
            case 2:
                score += 200;
                bufferPrefab = prefab200;
                break;
            case 3:
                score += 400;
                bufferPrefab = prefab400;
                break;
            case 4:
                score += 800;
                bufferPrefab = prefab800;
                break;
            case 5:
                score += 1000;
                bufferPrefab = prefab1000;
                break;
            case 6:
                score += 2000;
                bufferPrefab = prefab2000;
                break;
            case 7:
                score += 4000;
                bufferPrefab = prefab4000;
                break;
            case 8:
                score += 8000;
                bufferPrefab = prefab8000;
                break;
        }

        m_currentScore += score;
        text.text = "" + m_currentScore;

        GameObject scoreDisplay = GameObject.Instantiate(bufferPrefab, Input.mousePosition, new Quaternion(0, 0, 0, 0), transform.parent);
        scoreDisplay.GetComponent<ScoreBehaviour>().ActivateScoreBehaviour();
        m_lastTimeDeath = Time.time;
    }
}
