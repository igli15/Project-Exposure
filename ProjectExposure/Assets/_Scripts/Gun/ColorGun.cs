using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGun : AbstractGun 
{
	
	public override void Start()
	{
		base.Start();
		
		m_material.SetColor("_Color",Color.red);
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	protected override void Shoot()
	{
		
	}
}
