using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandleScript : MonoBehaviour , IPointerExitHandler
{

	[SerializeField] private Slider m_slider;
	
	[SerializeField] private Sprite m_normalHandleSprite;
	[SerializeField] private Sprite m_leftHandleSprite;
	[SerializeField] private Sprite m_rightHandleSprite;

	private Image m_image;

	private float m_oldValue;
	
	// Use this for initialization
	void Start ()
	{
		m_image = GetComponent<Image>();
		m_oldValue = m_slider.value;
		
		m_slider.onValueChanged.AddListener((delegate(float value)
		{
			if (m_oldValue < value)
			{
				m_image.sprite = m_rightHandleSprite;
			}
			else if (m_oldValue > value)
			{
				m_image.sprite = m_leftHandleSprite;
			}

			m_oldValue = value;
		}));
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	public void OnPointerExit(PointerEventData eventData)
	{
		m_image.sprite = m_normalHandleSprite;
	}
}
