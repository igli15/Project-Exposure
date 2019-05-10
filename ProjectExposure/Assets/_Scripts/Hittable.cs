using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hittable : MonoBehaviour
{
	public Action<Hittable> OnHit;
	
	public Action<Hittable> OnAimed;

	public Color color;

	public virtual void HitByGun(float damage,Gun gun)
	{
		if (OnHit != null) OnHit(this);
	}

	public virtual void Aimed(Gun gun)
	{
		if (OnAimed != null) OnAimed(this);
	}
}
