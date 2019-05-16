using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorGun : Gun
{
	[SerializeField] private Renderer m_beamRenderer;

	public float GetHueOfColor(Color color)
	{
		float hue = 0;
		float saturation = 0;
		float value = 0;
		
		Color.RGBToHSV(color, out hue, out saturation, out value);

		return hue;
	}

	public Vector3 GetHSVOfAColor(Color color)
	{
		float hue = 0;
		float saturation = 0;
		float value = 0;
		
		Color.RGBToHSV(color, out hue, out saturation, out value);

		return new Vector3(hue,saturation,value);
	}

	public override void SetColor(Color newColor)
	{
		base.SetColor(newColor);

		m_material.SetColor("_Color", newColor);
		m_beamRenderer.material.color = newColor;
		m_color = newColor;

		float hue = GetHueOfColor(newColor);

		if (hue >= 0.95f) hue = 0; //just reset the red hue range.
		m_beamRenderer.material.SetFloat("_Wavelength",(hue -2)  * -2);
	}
}
