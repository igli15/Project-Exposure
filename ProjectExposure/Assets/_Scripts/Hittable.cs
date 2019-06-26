using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;

using UnityEngine;
using UnityEngine.Events;

public abstract class Hittable : MonoBehaviour
{
	public UnityEvent OnHit;
	

	[SerializeField] protected Color color;

	public virtual void SetColor(Color newColor)
	{
		color = newColor;
		GetComponent<Renderer>().material.color = newColor;
	}

	public virtual void Hit(AbstractGun gun, float damage)
	{
		OnHit.Invoke();

		if (gun is SingleGun)
		{
			Health health = GetComponent<Health>();
			if (health != null) health.InflictDamage(damage);
		}
		else if (gun is MergedGun)
		{
			Health health = GetComponent<Health>();
			
			if (health != null) health.InflictDamage(101);
		}
	}

	public Color GetColor()
	{
		return color;
	}
}
