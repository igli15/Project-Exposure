using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HudButton : TouchButton
{
	private Image[] m_images;

	// Use this for initialization
	void Start()
	{
		m_images = GetComponentsInChildren<Image>();
	}

	public void FillButton()
	{
		foreach (var image in m_images)
		{
			image.DOFillAmount(1.0f, 0.3f);
		}
	}
	
	public void UnFillButton()
	{
		foreach (var image in m_images)
		{
			image.DOFillAmount(0f, 0.3f);
		}
	}
}
