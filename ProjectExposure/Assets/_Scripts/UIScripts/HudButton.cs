using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HudButton : TouchButton
{
	private Image[] m_images;

	// Use this for initialization
	void Awake()
	{
		m_images = GetComponentsInChildren<Image>();
		
		foreach (var image in m_images)
		{
			image.fillAmount = 0;
		}
		gameObject.SetActive(false);
	}

	public void FillButton(bool enable = false)
	{
		if(enable) gameObject.SetActive(true);
		
		foreach (var image in m_images)
		{
			image.DOFillAmount(1.0f, 0.3f);
		}
	}
	
	public void UnFillButton(bool disable = false)
	{
		Tween tween = null;
		foreach (var image in m_images)
		{
			tween = image.DOFillAmount(0f, 0.3f);
		}
		
		if(tween != null) tween.onComplete += delegate { if(disable) gameObject.SetActive(false); };
	}
}
