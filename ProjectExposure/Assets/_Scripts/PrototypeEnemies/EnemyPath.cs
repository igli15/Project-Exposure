using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {

    //Initial number of enemies spawn
    public int archerSpawnCount = 0;
    public int tankSpawnCount = 0;
    public int healerSpawnCount = 0;

    public Action<EnemyFSM> onEnemyDeath;
    
    private int m_deathCount = 0;

    public void InitialSpawn()
    {
        for (int i = 0; i < archerSpawnCount; i++)
        {
            CreateArcher();
        }
    }

    public GameObject CreateArcher()
    {
        GameObject newEnemy = ObjectPooler.instance.SpawnFromPool("Archer", transform.position, transform.rotation);
        newEnemy.GetComponent<ArcherMovementState>().path = GetComponent<Path>();
        newEnemy.GetComponent<ArcherFSM>().onDeath += EnmyDied;
        newEnemy.GetComponent<ArcherFSM>().InitializeEnemy();
        return newEnemy;
    }

    public void EnmyDied(EnemyFSM fsm)
    {
        m_deathCount++;
        Debug.Log(fsm.gameObject.name + " died");
        if (onEnemyDeath != null) onEnemyDeath(fsm);
    }
}
