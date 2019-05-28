using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStats : MonoBehaviour
{
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

    private float m_lastTimeDeath = 0;
    public void Start()
    {
        instance = this;
    }

    private void Update()
    {
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

        GameObject bufferPrefab=null;
        switch (m_currentBonus) {
            case 1:
                bufferPrefab = prefab100;
                break;
            case 2:
                bufferPrefab = prefab200;
                break;
            case 3:
                bufferPrefab = prefab400;
                break;
            case 4:
                bufferPrefab = prefab800;
                break;
            case 5:
                bufferPrefab = prefab1000;
                break;
            case 6:
                bufferPrefab = prefab2000;
                break;
            case 7:
                bufferPrefab = prefab4000;
                break;
            case 8:
                bufferPrefab = prefab8000;
                break;
        }
        GameObject scoreDisplay = GameObject.Instantiate(bufferPrefab, Input.mousePosition, new Quaternion(0, 0, 0, 0), transform.parent);
        scoreDisplay.GetComponent<ScoreBehaviour>().ActivateScoreBehaviour();
        m_lastTimeDeath = Time.time;
    }
}
