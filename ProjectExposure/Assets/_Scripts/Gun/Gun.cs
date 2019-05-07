using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{

	public Action<Gun> OnChargeChanged;
	
	[SerializeField] 
	private Vector2 m_gunColorRange = Vector2.zero;

	[SerializeField] 
	private Color m_gunColor;

	[SerializeField] 
	private float m_gunChargeSpeed = 10;
	
	[SerializeField]
	private KeyCode m_keyCode;

	private Renderer m_renderer;

	private float m_hue = 0;
	
	// Use this for initialization
	void Start ()
	{
		m_renderer = GetComponent<Renderer>();
		
		m_renderer.material.color = m_gunColor;
		
		m_hue = GetColorHue(m_renderer.material.color) * 360;
		if(OnChargeChanged != null) OnChargeChanged(this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	
	float GetColorHue(Color color)
	{
		float hue = 1;
		float saturation = 0;
		float value = 0;
		
		Color.RGBToHSV(color,out hue,out saturation,out value);
		return hue;
	}

	public void IncreaseCharge()
	{
		if (m_hue < m_gunColorRange.y)
		{
			m_hue += m_gunChargeSpeed * Time.deltaTime;
			m_renderer.material.color = ChangeHue(m_gunColor, m_hue);
			if(OnChargeChanged != null) OnChargeChanged(this);
		}
	}

	public void DecreaseCharge()
	{
		if (m_hue > m_gunColorRange.x)
		{
			m_hue -= m_gunChargeSpeed * Time.deltaTime;
			m_renderer.material.color = ChangeHue(m_gunColor, m_hue);
			if(OnChargeChanged != null) OnChargeChanged(this);
		}
	}

	Color ChangeHue(Color color,float newHue)
	{
		float hue = 0;
		float saturation = 0;
		float value = 0;
		
		Color.RGBToHSV(color,out hue,out saturation,out value);

	
		return Color.HSVToRGB(newHue/360, saturation, value);
	}

	public float Hue()
	{
		return m_hue;
	}
}
