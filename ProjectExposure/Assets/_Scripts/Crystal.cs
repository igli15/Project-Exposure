using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Crystal : Hittable
{

	[SerializeField] private float m_explosionRadius = 5;
	
	// Use this for initialization
	void Start () 
	{
		OnReleased += delegate(Hittable hittable) { GetComponent<Rigidbody>().useGravity = true; };
		SetColor(color);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	public override void Hit(GunManager gunManager)
	{
		if (OnHit != null) OnHit(this);
		
		Debug.Log(gunManager.currentMode);
		if (gunManager.currentMode == GunManager.GunMode.COLOR)
		{
			SetColor(gunManager.colorGun.GetColor());
		}
		else if (gunManager.currentMode == GunManager.GunMode.MAGNET && gunManager.magnetGun.pulledHittable == null)
		{
			gunManager.magnetGun.PullTarget(this);
		}
		else if (gunManager.currentMode == GunManager.GunMode.MERGED && gunManager.colorGun.GetColor() == color)
		{
			Explode(gunManager);
			Destroy(transform.gameObject);
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
