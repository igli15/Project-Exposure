using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {

    //Initial number of enemies spawn
    public int archerSpawnCount = 0;
    public int tankSpawnCount = 0;
    public int healerSpawnCount = 0;

    private int m_deathCount = 0;

    public void InitialSpawn()
    {
        for (int i = 0; i < archerSpawnCount; i++)
        {
            CreateSimpleEnemy();
        }
    }

    public GameObject CreateSimpleEnemy()
    {
        GameObject newEnemy = ObjectPooler.instance.SpawnFromPool("SimpleEnemy", transform.position, transform.rotation);
        newEnemy.GetComponent<EnemyMovementState>().path = GetComponent<Path>();
        return newEnemy;
    }

    public void OnEnemyDeath(EnemyFSM fsm)
    {
        m_deathCount++;

        Debug.Log(fsm.gameObject.name + " died");
    }
}
