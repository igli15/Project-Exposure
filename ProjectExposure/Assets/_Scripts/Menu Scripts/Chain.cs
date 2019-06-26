using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : Raycastable
{
	private Rigidbody m_rigidbody;

	private void Start()
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}

	public override void Click(Ray ray)
	{
		base.Click(ray);
		
		m_rigidbody.AddForce(ray.direction * 50,ForceMode.Impulse);
	}
}
