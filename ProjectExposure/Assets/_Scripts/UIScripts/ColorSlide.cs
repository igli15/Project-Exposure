using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSlide : MonoBehaviour 
{

	[SerializeField] 
	private Gun targetGun;

	private Slider m_slider;
	
	// Use this for initialization
	void Start ()
	{
		m_slider = GetComponent<Slider>();
		m_slider.onValueChanged.AddListener(delegate { UpdateGunHue(); });
		
		m_slider.onValueChanged.Invoke(m_slider.value);
		
		targetGun.OnChargeChanged += delegate(Gun gun) { m_slider.value = gun.Hue() / 300;  };
	}

	void UpdateGunHue()
	{
		targetGun.SetHue(m_slider.value);
	}
	
}
