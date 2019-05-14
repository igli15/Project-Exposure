using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hittable : MonoBehaviour
{
	public Action<Hittable> OnHit;

	[SerializeField] protected Color color;

	public virtual void HitByGun(Gun gun)
	{
		if (OnHit != null) OnHit(this);
	}


	public virtual void SetColor(Color newColor)
	{
		color = newColor;
		GetComponent<Renderer>().material.color = newColor;
	}

	public Color GetColor()
	{
		return color;
	}
}
