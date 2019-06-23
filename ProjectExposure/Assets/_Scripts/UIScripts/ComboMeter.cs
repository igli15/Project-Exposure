using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
		
		IncreaseFill(20);
	}


	public void IncreaseFill(float degrees)
	{
		m_image.DOFillAmount(m_image.fillAmount + (degrees / 360.0f), 0.25f);
		float fillDegrees = (m_image.fillAmount) * 360 + degrees;

		int index = (int)(fillDegrees / 36.0f) - 1;
		
		if(index >= 0)
		m_fillFrames[index].gameObject.SetActive(true);

	}
}
