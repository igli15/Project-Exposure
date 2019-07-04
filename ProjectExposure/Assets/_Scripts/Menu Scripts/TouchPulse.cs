using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TouchPulse : MonoBehaviour
{
	private Image m_image;
	
	private Sequence m_sequence;
	
	private float m_initAlpha;
	private Vector3 m_initScale;
	private RectTransform m_rectTransform;

	private void Awake()
	{
		m_image = GetComponent<Image>();
		//m_image.DOFade(0,0);

		m_initAlpha = m_image.color.a;
		m_rectTransform = GetComponent<RectTransform>();
		m_initScale = m_rectTransform.localScale;
		
		m_sequence = DOTween.Sequence();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Spawn(Input.mousePosition);
		}
	}

	private void Spawn(Vector3 pos)
	{

		transform.position = pos;
		
		if(m_sequence.IsPlaying()) m_sequence.Restart();
		else
		{
			m_sequence.Append(m_image.transform.DOPunchScale(m_initScale, 0.5f));
			m_sequence.Append(transform.DOScale(Vector3.zero, 0.5f));
		}
	}

	
	
}
