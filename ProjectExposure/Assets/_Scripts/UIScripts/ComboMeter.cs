using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ComboMeter : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI m_multiplierText;

	[SerializeField] private Image[] m_fillFrames;

	private Image m_image;

	private int m_multiplier = 1;

	public int multiplier
	{
		get { return m_multiplier; }
	}

	// Use this for initialization
	void Start ()
	{
		m_image = GetComponent<Image>();
		
		
		
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			IncreaseFill(40);
		}
		
		if (Input.GetKeyDown(KeyCode.B))
		{
			BreakCombo();
		}
	}


	public void IncreaseFill(float degrees)
	{
		m_image.DOFillAmount(m_image.fillAmount + (degrees / 360.0f), 0.3f);
		float fillDegrees = (m_image.fillAmount) * 360 + degrees;

		int index = (int)(fillDegrees / 36.0f) - 1;
		
		if(index >= 0 && index <= 9) 
			m_fillFrames[index].gameObject.SetActive(true);
		
		
		if (index >= 9 && m_multiplier < 9)
		{
			IncreaseMultiplier();
		}
		

	}

	public void BreakCombo()
	{
		Reset();
		m_multiplier = 1;
	}
	
	private void IncreaseMultiplier()
	{
		Reset();
		m_multiplier += 1;
		m_multiplierText.text = m_multiplier + "X";
		m_multiplierText.transform.DOPunchScale(Random.insideUnitCircle * 2, 0.2f);
	}


	private void Reset()
	{
		DOTween.KillAll();
		for (int i = 0; i < m_fillFrames.Length; i++)
		{
			m_fillFrames[i].gameObject.SetActive(false);
		}

		m_image.fillAmount = 0;
		m_multiplierText.text = "1X";
	}
	
}
