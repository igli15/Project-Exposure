using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : TouchButton
{

	[SerializeField] private Color m_color;
	
	private Image m_image;

	private ColorButtonManager m_manager;
	
	// Use this for initialization
	void Start()
	{
		m_image = GetComponent<Image>();
		m_image.color = m_color;
		m_manager = GetComponentInParent<ColorButtonManager>();

		Gun gun = m_manager.gun;
		OnTouchEnter.AddListener(delegate { gun.ChangeColor(m_color); });
	}

	// Update is called once per frame
	void Update () 
	{
		
	}
}
