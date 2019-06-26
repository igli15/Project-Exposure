using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
	private TextMeshProUGUI m_textMeshPro;
	
	// Use this for initialization
	void Start ()
	{
		m_textMeshPro = GetComponent<TextMeshProUGUI>();
		m_textMeshPro.text = "YOUR SCORE: " + "\n" + HighScoreManager.instance.highScore;
	}
	

}
