using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Crystal : Hittable
{

	public Action<Crystal> OnExplode;

	[SerializeField] private float m_explosionRadius = 5;

	[SerializeField] private AoeSphere m_aoeSphere;
	
	
	// Use this for initialization
	void Start () 
	{
		OnReleased += delegate(Hittable hittable) { GetComponent<Rigidbody>().useGravity = true; };
		OnPulled += delegate(Hittable hittable) {   GetComponent<Rigidbody>().velocity = Vector3.zero;};
		SetColor(color);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	public override void Hit(GunManager gunManager,float damage)
	{
		if (OnHit != null) OnHit(this);
		
		if (gunManager.currentMode == GunManager.GunMode.COLOR)
		{
			SetColor(gunManager.colorGun.GetColor());
		}
		else if (gunManager.currentMode == GunManager.GunMode.SHOOT && gunManager.CheckIfColorAreSimilar(gunManager.colorGun.GetColor() ,color,40))
		{
			if (damage > 0.2f)
			{
				Explode(gunManager);
				if (OnExplode != null) OnExplode(this);
				Instantiate(m_aoeSphere.gameObject, transform.position, Quaternion.identity);
				Destroy(transform.gameObject);
			}
		}
		
	}


	public void Explode(GunManager gunManager)
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, m_explosionRadius);

		for (int i = 0; i < colliders.Length; i++)
		{
			Hittable hittable = colliders[i].GetComponent<Hittable>();
			if( hittable!= null)
			{
				Health h = colliders[i].GetComponent<Health>();
				if (h != null)
				{
					h.InflictDamage(gunManager.CalculateDamage(color,hittable.GetColor()));
				}
			}
		}
	}
}
