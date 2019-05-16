using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPointManager : MonoBehaviour {

	public int pointNumber=0;
	private FightPoint m_fightPoint;
	private EnemyPath m_enemyPath;
	void Start () {
		if(pointNumber==0)
		{
			m_fightPoint = GetComponent<FightPoint>();
			m_enemyPath =  m_fightPoint.GetEnemyPathByName("EnemyPath_0");
			Debug.Log("EnemyPath "+m_enemyPath);
			m_enemyPath.onEnemyDeath += OnEnemyDeath;
			//m_enemyPath.onPointActivated+=OnPointActivated;
		}
		if(pointNumber==1)
		{
			m_fightPoint = GetComponent<FightPoint>();
			m_enemyPath =  m_fightPoint.GetEnemyPathByName("EnemyPath_0");
			Debug.Log("EnemyPath "+m_enemyPath);
			m_enemyPath.onEnemyDeath += OnEnemyDeath;
		}
	}
	void Update () {
		if(pointNumber==1)
		{
			int deathNumber=0;
			foreach(EnemyPath ep in m_fightPoint.GetAllEnemyPaths())
			{
				if(ep.deathCount>0) deathNumber++;
			}
			if(deathNumber>=4) RailMovement.instance.StartMovement();
		}
	}


	void OnEnemyDeath(EnemyFSM enemyFSM)
	{
		if(pointNumber==0)
		{
			Debug.Log("enemy died: "+m_enemyPath.deathCount);
			if(m_enemyPath.deathCount == 1)
			{
			m_enemyPath.CreateArcher(Color.white);
			}
			if(m_enemyPath.deathCount >=2)
			{
				RailMovement.instance.StartMovement();
			}
		}

	}
}
