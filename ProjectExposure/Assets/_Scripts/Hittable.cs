using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public abstract class Hittable : MonoBehaviour
{
	public Action<Hittable> OnHit;
	
	public Action<Hittable> OnPulled;
	public Action<Hittable> OnPushed;

	public Action<Hittable> OnReleased;

	[SerializeField] protected Color color;

	public virtual void SetColor(Color newColor)
	{
		color = newColor;
		GetComponent<Renderer>().material.color = newColor;
	}

	public virtual void Hit(GunManager gunManager)
	{
		if (OnHit != null) OnHit(this);
		
		if (gunManager.currentMode == GunManager.GunMode.COLOR)
		{
			SetColor(gunManager.colorGun.GetColor());
		}
		else if (gunManager.currentMode == GunManager.GunMode.MAGNET && gunManager.magnetGun.pulledHittable == null)
		{
			gunManager.magnetGun.PullTarget(this);
		}
		else
		{
			Health health = GetComponent<Health>();
			if(health != null) health.InflictDamage(gunManager.damage);
		}
	}

	public Color GetColor()
	{
		return color;
	}
}
