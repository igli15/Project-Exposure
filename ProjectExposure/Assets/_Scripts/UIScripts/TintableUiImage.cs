using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TintableUiImage : MonoBehaviour
{

	[SerializeField] [Range(0,1)] private float m_alpha = 1;

	private Image m_image;

	public void Awake()
	{
		m_image = GetComponent<Image>();
		GunManager.OnColorChanged += ChangeColor;
	}

	private void ChangeColor(Color newColor)
	{
		Color c = newColor;
		c.a = m_alpha;
		m_image.color = c;
	}

	private void OnDestroy()
	{
		GunManager.OnColorChanged -= ChangeColor;
	}
}
