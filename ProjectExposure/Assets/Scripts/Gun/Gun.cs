using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

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
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(m_keyCode))
		{

			if (Input.GetKey(KeyCode.UpArrow) && m_hue < m_gunColorRange.y)
			{
				m_hue += m_gunChargeSpeed * Time.deltaTime;
			}
			else if (Input.GetKey(KeyCode.DownArrow) && m_hue > m_gunColorRange.x)
			{
				m_hue -= m_gunChargeSpeed * Time.deltaTime;
			}

			m_renderer.material.color = ChangeHue(m_gunColor, m_hue);
		}
	}

	
	float GetColorHue(Color color)
	{
		float hue = 0;
		float saturation = 0;
		float value = 0;
		
		Color.RGBToHSV(color,out hue,out saturation,out value);
		return hue;
	}

	Color ChangeHue(Color color,float newHue)
	{
		float hue = 0;
		float saturation = 0;
		float value = 0;
		
		Color.RGBToHSV(color,out hue,out saturation,out value);

		
		Debug.Log(newHue);
		
		return Color.HSVToRGB(newHue/360, saturation, value);
	}
}
