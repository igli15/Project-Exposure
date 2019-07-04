using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnEnemy : MonoBehaviour {

    [SerializeField]
    private List<GameObject> m_enemies = new List<GameObject>();
    [SerializeField]
    private bool m_withDelay = false;
    [SerializeField]
    private float m_delay = 2.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (m_withDelay == false)
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
        else
        {
            Sequence s = DOTween.Sequence();
            foreach (GameObject enemy in m_enemies)
            {

                if (enemy != null && other.CompareTag("Player"))
                {
                    s.Append(DOVirtual.DelayedCall(m_delay, delegate {
                        enemy.gameObject.SetActive(true);
                        enemy.GetComponent<ArcherFSM>().InitializeEnemy();
                    }));
                   
                }


            }
        }
    }

}
