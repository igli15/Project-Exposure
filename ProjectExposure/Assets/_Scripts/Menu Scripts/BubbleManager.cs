using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{

	[SerializeField] private ScoreBubble[] m_scoreBubbles;
	
	// Use this for initialization
	void Start () 
	{
		HighScoreManager.instance.LoadHighScores();
		
		var scores = HighScoreManager.instance.orderedScores;
		

		int count = 0;
		if (scores.Count() > 10) count = 10;
		else
		{
			count = scores.Count();
			
		}
		
		for (int i = 0; i < count ; i++)
		{
			string s = scores.ElementAt(i).Key + ":" + "\n" + scores.ElementAt(i).Value ;
			m_scoreBubbles[i].Spawn(s);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
