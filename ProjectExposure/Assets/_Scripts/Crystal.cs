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
	
	public float explosionRadius
	{
		get { return m_explosionRadius; }
	}

	public bool exploded
	{
		get { return m_exploded; }
	}

	// Use this for initialization
	void Start () 
	{
		//OnReleased += delegate(Hittable hittable) { GetComponent<Rigidbody>().useGravity = true; };
		//OnPulled += delegate(Hittable hittable) {   GetComponent<Rigidbody>().velocity = Vector3.zero;};
		SetColor(color);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	public override void Hit(GunManager gunManager,float damage,Color gunColor)
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
				if (damage > 0.2f)
				{
					Explode(gunManager);
				}
			}
		}
		else if (gunManager.fsm.GetCurrentState() is MergedGunsState)
		{
			Explode(gunManager);
		}
	}


	public void AoeOverlapSphere(GunManager gunManager)
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
					DOVirtual.DelayedCall(0.1f, () =>
					{
						Crystal c = hittable.GetComponent<Crystal>();
						if (!c.exploded)
						{
							c.Explode(gunManager);
						}
					});
				}
				
				Health h = colliders[i].GetComponent<Health>();
				if (h != null)
				{
					h.InflictDamage(gunManager.CalculateDamage(color, hittable.GetColor()));
				}

			}
			
		}
	}

	public void Explode(GunManager gunManager)
	{
		AoeOverlapSphere(gunManager);
		if (OnExplode != null) OnExplode(this);
        Instantiate(m_crystalExplosionPrefab.gameObject, transform.position, Quaternion.identity);
        ScoreStats.instance.AddDeathData(GetColor(),transform, 2);
		Destroy(transform.gameObject);
	}

}
