using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderElement : MonoBehaviour
{
	private bool m_isFull = false;

	private DiscreteSlider m_parentSlider;

	private SliderElement m_previousNeighbour;

	private Image m_image;

	private void Start()
	{
		m_image = GetComponent<Image>();
	}

	void Fill()
	{
		
	}

	void Empty()
	{
		
	}
	
}
