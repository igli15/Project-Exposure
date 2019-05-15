using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSlider : MonoBehaviour 
{

	[SerializeField] private GunManager m_gunManager;

	private Slider m_slider;
	
	// Use this for initialization
	void Start ()
	{
		m_slider = GetComponent<Slider>();
		m_slider.onValueChanged.AddListener(delegate { UpdateGunHue(); });
		
		m_slider.onValueChanged.Invoke(m_slider.value);
		
	}

	void UpdateGunHue()
	{
		float h = m_slider.value * 270.0f / 360.0f;
		
	
		m_gunManager.SetGunColors(Color.HSVToRGB(h ,1,1));
	}
	
}
