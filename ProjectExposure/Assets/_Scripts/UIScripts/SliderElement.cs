using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderElement : MonoBehaviour,IPointerClickHandler
{
	private bool m_isFull = false;

	private DiscreteSlider m_parentSlider;

	private Image m_image;

	private int m_index = -1;

	public int index
	{
		get { return m_index; }
		set { m_index = value; }
	}

	public bool isFull
	{
		get { return m_isFull; }
	}

	private void Awake()
	{
		m_image = GetComponent<Image>();
	}

	public void Fill()
	{
		m_image.sprite = m_parentSlider.filledSprite;
		m_isFull = true;
	}

	public void Empty()
	{
		m_image.sprite = m_parentSlider.emptySprite;
		m_isFull = false;
	}

	public void SetParentSlider(DiscreteSlider slider)
	{
		m_parentSlider = slider;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		m_parentSlider.SelectElement(this);
	}
}
