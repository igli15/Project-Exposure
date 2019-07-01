using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.UIElements;

public class DiscreteSlider : MonoBehaviour
{
	[System.Serializable]
	public class ValueChangedEvent : UnityEvent<int>
	{
	}

	public ValueChangedEvent OnValueChanged;

	[SerializeField] private Sprite m_emptySprite;
	[SerializeField] private Sprite m_filledSprite;

	[SerializeField] private SliderElement[] m_sliderElements;

	private int m_value = 0;

	public Sprite emptySprite
	{
		get { return m_emptySprite; }
	}
	
	public Sprite filledSprite
	{
		get { return m_filledSprite; }
	}

	public int value
	{
		get { return m_value; }
	}

	private void Start()
	{
		if (OnValueChanged == null) OnValueChanged = new ValueChangedEvent();
		for (int i = 0; i < m_sliderElements.Length ; i++)
		{
			m_sliderElements[i].SetParentSlider(this);
			m_sliderElements[i].index = i;
		}
		
		FillElement(m_sliderElements[0]);
	}

	public void SelectElement(SliderElement sliderElement)
	{
		FillElement(sliderElement);

		int index = sliderElement.index;

		if (index < m_sliderElements.Length - 1 && m_sliderElements[index + 1].isFull)
		{
			RebaseSlider(sliderElement);
		}
		else
		{
			for (int i = sliderElement.index - 1; i >= 0; i--)
			{
				FillElement(m_sliderElements[i]);
			}
		}
	}
	
	private void RebaseSlider(SliderElement element)
	{
		for (int i = element.index + 1; i < m_sliderElements.Length ; i++)
		{
			EmptyElement(m_sliderElements[i]);
		}	
	}

	private void FillElement(SliderElement sliderElement)
	{
		if (!sliderElement.isFull)
		{
			sliderElement.Fill();
			m_value += 1;
			if(OnValueChanged != null) OnValueChanged.Invoke(m_value);
		}
	}
	
	private void EmptyElement(SliderElement sliderElement)
	{
		if (sliderElement.isFull)
		{
			sliderElement.Empty();
			m_value -= 1;
			if(OnValueChanged != null) OnValueChanged.Invoke(m_value);
		}
	}
}
