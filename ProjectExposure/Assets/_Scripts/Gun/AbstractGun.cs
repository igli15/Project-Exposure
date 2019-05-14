using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGun : MonoBehaviour,IAgent
{
	public Action<AbstractGun> OnShoot;

	public Action<AbstractGun> OnColorChanged;
	
	protected Color m_color;

	protected Material m_material;

	protected Fsm<AbstractGun> m_fsm;
	
	// Use this for initialization
	public virtual void Start ()
	{
		m_material = GetComponent<Renderer>().material;

		if (m_fsm == null)
		{
			m_fsm = new Fsm<AbstractGun>(this);
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if (Input.GetMouseButton(0))
		{
			Shoot();
		}
	}

	protected abstract void Shoot();



	public Color GetColor()
	{
		return m_color;
	}

	public virtual void SetColor(Color newColor)
	{
		m_color = newColor;
		m_material.SetColor("_Color", newColor);
		if (OnColorChanged != null) OnColorChanged(this);
	}
}
