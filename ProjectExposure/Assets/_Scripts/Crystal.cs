using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;

public class Crystal : Hittable
{
	public Action<Crystal> OnExplode;

	[SerializeField] private float m_explosionRadius = 5;

	[SerializeField] private GameObject m_crystalExplosionPrefab;

	private bool m_exploded = false;

	private float m_delay = 0.25f;
	
	public float explosionRadius
	{
		get { return m_explosionRadius; }
	}

	public bool exploded
	{
		get { return m_exploded; }
	}

	// Use this for initialization
	public void Start () 
	{
		//OnReleased += delegate(Hittable hittable) { GetComponent<Rigidbody>().useGravity = true; };
		//OnPulled += delegate(Hittable hittable) {   GetComponent<Rigidbody>().velocity = Vector3.zero;};
		SetColor(color);
	}
	
	// Update is called once per frame
	void Update () 
	{
	}


	public override void Hit(AbstractGun gun,float damage)
	{
		OnHit.Invoke();

		if (damage > 0.2f)
		{
			Explode(gun);
		}
		
	}


	public void AoeOverlapSphere(AbstractGun gun)
	{
		m_exploded = true;
		Collider[] colliders = Physics.OverlapSphere(transform.position, m_explosionRadius);

		for (int i = 0; i < colliders.Length; i++)
		{
			Hittable hittable = colliders[i].GetComponent<Hittable>();
			if( hittable!= null)
			{
				if (hittable.CompareTag("Crystals") && hittable.gameObject.GetInstanceID() != gameObject.GetInstanceID())
				{
					DOVirtual.DelayedCall(m_delay, () =>
					{
						Crystal c = hittable.GetComponent<Crystal>();
						if (!c.exploded)
						{
							c.Explode(gun);
						}
					});
				}
				
				Health h = colliders[i].GetComponent<Health>();
				if (h != null)
				{
					h.InflictDamage(gun.manager.CalculateDamage(color, hittable.GetColor()));
				}

			}
			
		}
	}

	public void Explode(AbstractGun gun)
	{
		if (gun.manager.fsm.GetCurrentState() is SplitGunsState)
		{
			(gun.manager.fsm.GetCurrentState() as SplitGunsState).AddCollectedColor(GetColor());
		}
		
		AoeOverlapSphere(gun);
		if (OnExplode != null) OnExplode(this);
        Instantiate(m_crystalExplosionPrefab.gameObject, transform.position, Quaternion.identity);
        ScoreStats.instance.AddDeathData(GetColor(),transform,true);
		Destroy(transform.gameObject);
	}

}
