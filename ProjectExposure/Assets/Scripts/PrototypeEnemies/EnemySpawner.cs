using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public RailMovement railMovement;

    [SerializeField]
    private List<GameObject> m_enemies;
    private int m_enemyNumber;
    private int m_currentCount=1;

    private void Start()
    {
        m_enemyNumber = m_enemies.Count;
        m_currentCount = m_enemyNumber;

        foreach (GameObject enemy in m_enemies)
        {
            enemy.SetActive(false);
            enemy.GetComponent<Health>().OnDeath += OnEnemyDeath;
        }
    }

    public void SpawnEnemies()
    {
        foreach (GameObject enemy in m_enemies)
        {
            enemy.SetActive(true);
        }
    }

    void OnEnemyDeath(Health health)
    {
        m_currentCount--;
        if (m_currentCount <= 0)
        {
            Debug.Log("ZONE IS CLEARED");
            if (railMovement != null) railMovement.StartMovement();
        }
    }
}
