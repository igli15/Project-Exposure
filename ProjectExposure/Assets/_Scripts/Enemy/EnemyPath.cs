using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {

    //Initial number of enemies spawn
    [SerializeField]
    private Color m_initColor;
    public int archerSpawnCount = 0;
    public int tankSpawnCount = 0;
    public int healerSpawnCount = 0;

    public Action<EnemyFSM> onEnemyDeath;
    
    private int m_deathCount = 0;
    public int deathCount{get{return m_deathCount;}}
    public void InitialSpawn()
    {
        for (int i = 0; i < archerSpawnCount; i++)
        {
            CreateArcher(m_initColor);
        }
    }

    public GameObject CreateArcher(Color color)
    {
        GameObject newEnemy = ObjectPooler.instance.SpawnFromPool("Archer", transform.position, transform.rotation);
        newEnemy.GetComponent<ArcherMovementState>().path = GetComponent<Path>();
        newEnemy.transform.position=newEnemy.GetComponent<ArcherMovementState>().path.GetFirstPoint().transform.position;
        newEnemy.GetComponent<Enemy>().SetColor(color);
        newEnemy.GetComponent<ArcherFSM>().onDeath += EnemyDied;
        newEnemy.GetComponent<ArcherFSM>().InitializeEnemy();
        return newEnemy;
    }

    public GameObject CreateHealer()
    {
        GameObject newEnemy = ObjectPooler.instance.SpawnFromPool("Healer", transform.position, transform.rotation);
        newEnemy.GetComponent<HealerMovementState>().path = GetComponent<Path>();
        newEnemy.transform.position = newEnemy.GetComponent<HealerMovementState>().path.GetFirstPoint().transform.position;
        //.GetComponent<Enemy>().SetColor(color);
        newEnemy.GetComponent<HealerFSM>().onDeath += EnemyDied;
        newEnemy.GetComponent<HealerFSM>().InitializeEnemy();
        return newEnemy;
    }

    public void EnemyDied(EnemyFSM fsm)
    {
        m_deathCount++;
        Debug.Log(fsm.gameObject.name + " died");
        if (onEnemyDeath != null) onEnemyDeath(fsm);
    }
}
