using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorGun : Gun
{
	[SerializeField] private Renderer m_beamRenderer;

	public override void SetColor(Color newColor)
	{
		base.SetColor(newColor);

		m_material.SetColor("_Color", newColor);
		m_beamRenderer.material.color = newColor;
		m_color = newColor;

		float hue = ColorUtils.GetHueOfColor(newColor);

		if (hue >= 0.95f) hue = 0; //just reset the red hue range.
		m_beamRenderer.material.SetFloat("_Wavelength",(hue -2)  * -2);
	}
}
