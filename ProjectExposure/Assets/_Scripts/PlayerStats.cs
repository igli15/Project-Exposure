using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
	private static PlayerStats m_instance;

	private float m_comboFill = 0;
	private int m_comboMultiplier = 1;
	private int m_score = 0;
	private bool m_hasMerged = false;

	public static PlayerStats instance
	{
		get { return m_instance; }
	}

	public float comboFill
	{
		get { return m_comboFill; }
		set { m_comboFill = value; }
	}

	public int comboMultiplier
	{
		get { return m_comboMultiplier; }
		set { m_comboMultiplier = value; }
	}

	public int score
	{
		get { return m_score; }
		set { m_score = value; }
	}

	public bool hasMerged
	{
		get { return m_hasMerged; }
		set { m_hasMerged = value; }
	}

	private void Awake()
	{
		if (m_instance == null)
		{
			m_instance = this;
		}
		else Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	}


	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 );
		}
	}

	public void ResetAllData()
	{
		m_comboFill = 0;
		m_comboMultiplier = 1;
		m_score = 0;
		m_hasMerged = false;
	}
}
