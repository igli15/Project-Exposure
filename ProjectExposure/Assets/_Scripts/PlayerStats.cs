using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	private static PlayerStats m_instance;

	private float m_comboFill = 0;
	private float m_comboMultiplier = 0;
	private float m_score = 0;
	private bool m_hasMerged = false;

	public float comboFill
	{
		get { return m_comboFill; }
		set { m_comboFill = value; }
	}

	public float comboMultiplier
	{
		get { return m_comboMultiplier; }
		set { m_comboMultiplier = value; }
	}

	public float score
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
}
