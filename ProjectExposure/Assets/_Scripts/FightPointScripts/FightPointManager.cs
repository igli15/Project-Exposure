using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPointManager : MonoBehaviour {


	private FightPoint m_fightPoint;
	private EnemyPath m_enemyPath;
	void Start () {
		m_fightPoint = GetComponent<FightPoint>();
		 m_enemyPath =  m_fightPoint.GetEnemyPathByName("EnemyPath_0");
		m_enemyPath.CreateArcher(Color.red);
		m_enemyPath.onEnemyDeath += OnEnemyDeath;
	}
	void Update () {
		
	}

	void OnEnemyDeath(EnemyFSM enemyFSM)
	{
		if(m_enemyPath.deathCount == 1)
		{
		m_enemyPath.CreateArcher(Color.green);
		}
		if(m_enemyPath.deathCount >=2)
		{
			RailMovement.instance.StartMovement();
		}
	}
}
