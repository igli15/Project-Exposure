using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PollEmoji : MonoBehaviour
{
	public enum Type
	{
		HAPPY,
		SAD
	}
	
	[SerializeField]
	private DiscreteSlider m_slider;

	[SerializeField] private Type m_type;

	private Image m_image;

	private void Start()
	{
		m_image = GetComponent<Image>();

		m_image.DOFade(0.3f, 0);
		
		m_slider.OnValueChanged.AddListener(OnSliderChanged);
	}

	private void OnSliderChanged(int value)
	{
		if (m_type == Type.HAPPY)
		{
			if (value >= 3)
			{
				m_image.DOFade(1, 0.2f);
			}
			else
			{
				m_image.DOFade(0.3f, 0.2f);
			}
		}
		else if (m_type == Type.SAD)
		{
			if (value < 3)
			{
				m_image.DOFade(1, 0.2f);
			}
			else
			{
				m_image.DOFade(0.3f, 0.2f);
			}
		}
	}
}
