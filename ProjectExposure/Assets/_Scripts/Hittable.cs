﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Events;

public abstract class Hittable : MonoBehaviour
{
	public UnityEvent OnHit;
	
	
	public Action<Hittable> OnPulled;
	public Action<Hittable> OnPushed;

	public Action<Hittable> OnReleased;

	[SerializeField] protected Color color;

	public virtual void SetColor(Color newColor)
	{
		color = newColor;
		GetComponent<Renderer>().material.color = newColor;
	}

	public virtual void Hit(GunManager gunManager, float damage,Color gunColor)
	{
		OnHit.Invoke();
		
		if (gunManager.currentMode == GunManager.GunMode.COLOR)
		{
			SetColor(gunColor);
		}
		else if (gunManager.currentMode == GunManager.GunMode.SHOOT)
		{
			Health health = GetComponent<Health>();
			if(health != null) health.InflictDamage(damage);
		}
	}

	public Color GetColor()
	{
		return color;
	}
}
