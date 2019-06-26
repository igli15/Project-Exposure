using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

    [SerializeField]
    private List<GameObject> m_enemies = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject enemy in m_enemies)
           {
            if (enemy != null && other.CompareTag("Player"))
            {
                enemy.gameObject.SetActive(true);
                enemy.GetComponent<ArcherFSM>().InitializeEnemy();
            }
        }
    }
}
