using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
	[SerializeField] 
	private Gun targetGun;
	
	[SerializeField]
	private RectTransform m_barRectTransform;
	
	private RectTransform m_transform;

	private float m_parentHalfWidth;

	private PointerScript m_otherPointer;

	public Action<PointerScript> OnPointerUpdated;

	private bool m_mouseEntered = false;
	
	// Use this for initialization
	void Awake ()
	{
		m_transform = GetComponent<RectTransform>();
		m_parentHalfWidth = m_barRectTransform.rect.width/2;

		targetGun.OnChargeChanged += SetPositionBasedOnHue;
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if (m_mouseEntered)
		{
			if (m_transform.anchoredPosition.x <= m_parentHalfWidth && m_transform.anchoredPosition.x >= -m_parentHalfWidth)
			{
				transform.position = Input.mousePosition;
			}
			else
			{
				if (m_transform.anchoredPosition.x < -m_parentHalfWidth)
				{
					m_transform.anchoredPosition = new Vector2(-m_parentHalfWidth,m_transform.anchoredPosition.y);
				}
				else if (m_transform.anchoredPosition.x > m_parentHalfWidth)
				{
					m_transform.anchoredPosition = new Vector2(m_parentHalfWidth, m_transform.anchoredPosition.y);
				}
			}

		}
		*/
	}


	void SetPositionBasedOnHue(Gun gun)
	{
		float pos = ((gun.Hue()/300) * 2 -1) * m_parentHalfWidth ; //flip it also
		
		m_transform.anchoredPosition = new Vector2(pos,m_transform.anchoredPosition.y);

		if (OnPointerUpdated != null) OnPointerUpdated(this);
	}

	void SetGunHue()
	{
		
	}

	float GetRelativePos()
	{
		float relativePos = (m_transform.anchoredPosition.x / m_parentHalfWidth + 1 )/2 ;
		                   
		return relativePos;
	}

	public Gun GetGun()
	{
		return targetGun;
	}
	
	public void OnPointerDown(PointerEventData eventData)
	{
		m_mouseEntered = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		m_mouseEntered = false;
	}
}
