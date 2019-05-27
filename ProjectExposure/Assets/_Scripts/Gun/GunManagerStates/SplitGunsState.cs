using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SplitGunsState : GunState
{
	[SerializeField] private Transform m_pullTargetLocation;
	
	[SerializeField] private float m_pushForce = 10;

	[SerializeField] private float m_pullForce = 50;
	
	[SerializeField] private GameObject[] objToDisable;

	[Range(0.1f,1)]
	[SerializeField] private float m_scaleDownFactor = 0.8f;

	private Hittable m_pulledHittable ;

	private bool m_targetIsInPlace ;

	private Tweener m_scaleTween;
	private Vector3 m_initScale;
	
	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);
		
		for (int i = 0; i < objToDisable.Length; i++)
		{
			objToDisable[i].SetActive(false);
		}
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
		
		for (int i = 0; i < objToDisable.Length; i++)
		{
			objToDisable[i].SetActive(true);
		}
	}

	public override void Shoot()
	{
		List<Hittable> hittables = target.RaycastFromGuns();

		if(hittables.Count>0 && !m_targetIsInPlace)PullTarget(hittables[0]);
		
		if (m_targetIsInPlace)
		{
			PushTarget(target.GetDirFromGunToMouse());
		}
	}

	protected override void Update()
	{
		base.Update();
		
		if (m_pulledHittable != null  && !m_targetIsInPlace)
		{
			if (Vector3.Distance(m_pulledHittable.transform.position, m_pullTargetLocation.position) < 0.5f)
			{
				m_targetIsInPlace = true;
				m_pulledHittable.transform.parent = m_pullTargetLocation;
			}
			
			m_pulledHittable.transform.position = Vector3.MoveTowards(m_pulledHittable.transform.position, m_pullTargetLocation.position,   m_pullForce*Time.deltaTime);
		}
		
	}

	public void PushTarget(Vector3 dir)
	{
		if (m_targetIsInPlace)
		{
			m_pulledHittable.transform.parent = null;
			m_pulledHittable.GetComponent<Rigidbody>().isKinematic = false;
			m_pulledHittable.GetComponent<Rigidbody>().AddForce(dir * m_pushForce, ForceMode.Impulse);
			
			if (m_pulledHittable.OnPushed != null) m_pulledHittable.OnPushed(m_pulledHittable);

			m_pulledHittable.transform.localScale = new Vector3(1,1,1);
			m_pulledHittable.transform.SetParent(null);
			m_pulledHittable = null;
			m_targetIsInPlace = false;
			
		}
	}

	public void ReleaseTarget()
	{
		if(m_pulledHittable.OnReleased != null) m_pulledHittable.OnReleased(m_pulledHittable);
		m_pulledHittable.transform.localScale = new Vector3(1,1,1);
		m_pulledHittable.GetComponent<Rigidbody>().velocity = Vector3.zero;
		m_pulledHittable.GetComponent<Rigidbody>().isKinematic = true;
		
		m_pulledHittable = null;
		m_targetIsInPlace = false;
	}

	public void PullTarget(Hittable t)
	{

		t.transform.localScale = t.transform.localScale * m_scaleDownFactor;
		
		t.GetComponent<Rigidbody>().velocity = Vector3.zero;
		t.GetComponent<Rigidbody>().isKinematic = true;
		m_pulledHittable = t;
		m_initScale = t.transform.localScale;

		if (t.OnPulled != null) t.OnPulled(t);
	}
}
