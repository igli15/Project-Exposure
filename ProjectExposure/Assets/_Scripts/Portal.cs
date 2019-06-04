using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private LevelLoader m_levelLoader;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			ScoreStats.instance.UpdateHighScore();
			//Debug.Log(HighScoreManager.instance.highScore);
			m_levelLoader.LoadLevel("ResolutionScene");
		}
	}
}
