using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : TouchButton
{

	[SerializeField] private Color m_color;

	[SerializeField] private GunManager m_gunManager;
	
	private Image m_image;

	// Use this for initialization
	void Start()
	{
		OnTouchEnter.AddListener(delegate { m_gunManager.SetGunColors(m_color); });
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
