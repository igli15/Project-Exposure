using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Gun : MonoBehaviour
{
	public Action<Gun> OnShoot;

	public Action<Gun> OnColorChanged;
	
	protected Color m_color;

	protected Material m_material;

	// Use this for initialization
	protected virtual void Start ()
	{
		m_material = GetComponent<Renderer>().material;

	}
	public Vector3 LookInRayDirection(Ray ray)
	{
		Ray r = ray;
		r.origin = transform.position;
		Quaternion rot = Quaternion.LookRotation(r.direction.normalized,Vector3.up);
		transform.DORotate(rot.eulerAngles, 0.5f);
		return r.direction;
	}

	public virtual void Shoot()
	{
		if (OnShoot != null) OnShoot(this);
	}

	public Color GetColor()
	{
		return m_color;
	}

	public virtual void SetColor(Color newColor)
	{
		m_color = newColor;
		
		if (OnColorChanged != null) OnColorChanged(this);
	}
}
