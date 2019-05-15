using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
