using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBehaviour1 : MonoBehaviour {

    FightPoint m_fightPoint;
	void Start () {
        m_fightPoint = GetComponent<FightPoint>();

        //Setting to each enemyPath in this fightPoint initial spawn count of archers to 1
        //So in the begining each enemyPath will spawn 1 Archer
        foreach (EnemyPath enemyPath in m_fightPoint.GetAllEnemyPaths())
        {
            enemyPath.archerSpawnCount = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            m_fightPoint.GetEnemyPathByName("EnemyPath_1").CreateArcher(Color.green);
            m_fightPoint.GetEnemyPathByName("EnemyPath_3").CreateArcher(Color.red);
        }
	}
}
