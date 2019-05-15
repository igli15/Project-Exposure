using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHud : MonoBehaviour
{

	[SerializeField] private GameObject m_rgbButtons;
	[SerializeField] private GameObject m_colorSlider;

	[SerializeField] private GunManager m_gunManager;
	
	// Use this for initialization
	void Awake ()
	{
		m_gunManager.OnMerge += delegate(GunManager manager)
		{
			m_rgbButtons.SetActive(false);
			m_colorSlider.SetActive(true);



			float hue = manager.colorGun.GetHueOfColor(manager.colorGun.GetColor());

			if (hue > 0.9f) hue = 0;
			float rangedHue = hue* 360.0f/270.0f;
			
			m_colorSlider.GetComponentInChildren<Slider>().value = rangedHue;
		};
		
		m_gunManager.OnSplit += delegate(GunManager manager)
		{
			m_rgbButtons.SetActive(true);
			m_colorSlider.SetActive(false);
		};
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
