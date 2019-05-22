using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {

    [SerializeField]
    private GameObject m_enemy;
    private void OnTriggerEnter(Collider other)
    {
        m_enemy.gameObject.SetActive(true);
        m_enemy.GetComponent<ArcherFSM>().InitializeEnemy();
    }
}
