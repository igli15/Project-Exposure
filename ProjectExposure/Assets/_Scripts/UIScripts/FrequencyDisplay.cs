using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencyDisplay : MonoBehaviour
{
	[SerializeField] private GunManager m_gunManager;
	[SerializeField] private Slider m_slider;
	
	private Material m_material;

	// Use this for initialization
	void Start ()
	{
		m_material = GetComponent<Image>().material;
		
		m_slider.onValueChanged.AddListener(delegate(float value)
		{
		
			float finalValue = Utils.Remap(value, 0, 1, 0.5f, 0.1f);
			
			m_material.SetFloat("_Wavelength", finalValue);
			
			float h = m_slider.value * 270.0f / 360.0f;

			Color c  = Color.HSVToRGB(h, 1, 1);

			//m_material.color = c;
			m_gunManager.SetGunColors(c);

		});
	
		
		m_slider.onValueChanged.Invoke(m_slider.value);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
