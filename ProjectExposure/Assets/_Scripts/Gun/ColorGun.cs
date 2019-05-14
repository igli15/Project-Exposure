﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorGun : Gun
{
	[SerializeField] private Renderer m_beamRenderer;
	
	protected override void Start()
	{
		base.Start();

		SetColor(Color.red);
	}

	protected override void HitAnHittable(Hittable hittable)
	{
		base.HitAnHittable(hittable);
		

		hittable.SetColor(m_color);

	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

	public float GetHueOfColor(Color color)
	{
		float hue = 1;
		float saturation = 0;
		float value = 0;

		Color.RGBToHSV(color, out hue, out saturation, out value);
		return hue;
	}

	public override void SetColor(Color newColor)
	{
		base.SetColor(newColor);

		m_beamRenderer.material.color = newColor;
		m_color = newColor;
		m_beamRenderer.material.SetFloat("_Wavelength",((GetHueOfColor(newColor) -2)  * -2));
	}
}
