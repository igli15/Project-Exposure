using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGun : MonoBehaviour
{
	public Action<AbstractGun> OnShoot;

	protected Color m_color;

	protected Material m_material;
	
	// Use this for initialization
	public virtual void Start ()
	{
		m_material = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton(0))
		{
			Shoot();
		}
	}

	protected abstract void Shoot();
	

}
