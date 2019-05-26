using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagnetGun : Gun
{	

	[SerializeField] private Transform m_pullTargetLocation;
	
	[SerializeField] private float m_pushForce = 10;

	private Hittable m_pulledHittable = null;

	private bool m_targetIsInPlace = false;

	private Tweener m_pullTween;
	public Hittable pulledHittable
	{
		get { return m_pulledHittable; }
		set { m_pulledHittable = value; }
	}

	public Transform pullTargetLocation
	{
		get { return m_pullTargetLocation; }
	}

	private void Awake()
	{
			
	}


	public void PushTarget(Vector3 dir)
	{
		if (m_targetIsInPlace)
		{
			m_pulledHittable.GetComponent<Rigidbody>().AddForce(dir * m_pushForce, ForceMode.Impulse);
			
			if (m_pulledHittable.OnPushed != null) m_pulledHittable.OnPushed(m_pulledHittable);
			
			m_pulledHittable = null;
			m_targetIsInPlace = false;
			
		}
	}

	public void ReleaseTarget()
	{
		m_pullTween.Kill();
		
		if(pulledHittable.OnReleased != null) pulledHittable.OnReleased(pulledHittable);
		m_pulledHittable = null;
		m_targetIsInPlace = false;
	}

	public void PullTarget(Hittable t)
	{
		m_pullTween =  t.transform.DOMove(m_pullTargetLocation.position, 2.0f);
		m_pulledHittable = t;
		m_pullTween.onComplete += delegate { m_targetIsInPlace = true; };
		if (t.OnPulled != null) t.OnPulled(t);
	}
}
