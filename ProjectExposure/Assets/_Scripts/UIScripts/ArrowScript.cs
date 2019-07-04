using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowScript : TouchButton
{

	enum ArrowType
	{
		LEFT,
		RIGHT
	}
	
	[SerializeField] private float m_valueIncreaseSpeed = 1.0f;

	[SerializeField] private Slider m_slider;
	
	[SerializeField] 
	private ArrowType m_arrowType;
	

	// Use this for initialization
	void Start () 
	{
		OnTouch.AddListener(UpdateSliders);
	}
	
	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

	void UpdateSliders()
	{
		if (m_arrowType == ArrowType.RIGHT)
		{
			m_slider.value -= m_valueIncreaseSpeed * Time.deltaTime;
		}
		else
		{
			m_slider.value += m_valueIncreaseSpeed * Time.deltaTime;
		}
	}

}
