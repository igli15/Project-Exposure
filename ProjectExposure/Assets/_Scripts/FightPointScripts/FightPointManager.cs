using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPointManager : MonoBehaviour {

	public int pointNumber=0;
	private FightPoint m_fightPoint;
	private EnemyPath m_enemyPath;
	private float m_cd =10.5f;
	private List<Color> m_listofColors = new List<Color>();
	void Start () {
		if(pointNumber==0)
		{
			m_fightPoint = GetComponent<FightPoint>();
			m_enemyPath =  m_fightPoint.GetEnemyPathByName("EnemyPath_0");
			Debug.Log("EnemyPath "+m_enemyPath);
			m_enemyPath.onEnemyDeath += OnEnemyDeath;
			//m_enemyPath.onPointActivated+=OnPointActivated;
			m_listofColors.Add(Color.red);
			m_listofColors.Add(Color.green);
			m_listofColors.Add(Color.blue);
			m_listofColors.Add(Color.yellow);
			m_listofColors.Add(Color.cyan);
			m_listofColors.Add(Color.magenta);
			m_listofColors.Add(Color.white);
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
		// if(pointNumber==1)
		// {
		// 	int deathNumber=0;
		// 	foreach(EnemyPath ep in m_fightPoint.GetAllEnemyPaths())
		// 	{
		// 		if(ep.deathCount>0) deathNumber++;
		// 	}
		// 	if(deathNumber>=4) RailMovement.instance.StartMovement();
		// }
		m_cd -= Time.deltaTime;
		if(m_cd < 0)
		{
			m_cd = 4.3f;
			int rndIndex = (int) Random.Range(0, m_listofColors.Count);
		 	m_enemyPath.CreateArcher(m_listofColors[rndIndex]);
		}
		

	}


	void OnEnemyDeath(EnemyFSM enemyFSM)
	{
	}
}
