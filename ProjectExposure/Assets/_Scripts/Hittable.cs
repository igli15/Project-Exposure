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

		if (gunManager.fsm.GetCurrentState() is SplitGunsState)
		{
			SplitGunsState splitGunsState = (gunManager.fsm.GetCurrentState() as SplitGunsState);
			
			if (splitGunsState.currentMode == SplitGunsState.GunMode.COLOR)
			{
				SetColor(gunColor);
			}
			else if (splitGunsState.currentMode == SplitGunsState.GunMode.SHOOT)
			{
				Health health = GetComponent<Health>();
				if (health != null) health.InflictDamage(damage);
			}
		}
		else if (gunManager.fsm.GetCurrentState() is MergedGunsState)
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
