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

	[SerializeField] protected Color color;

	public virtual void SetColor(Color newColor)
	{
		color = newColor;
		GetComponent<Renderer>().material.color = newColor;
	}

	public virtual void Hit(GunManager gunManager)
	{
		if (OnHit != null) OnHit(this);
	}

	public Color GetColor()
	{
		return color;
	}
}
