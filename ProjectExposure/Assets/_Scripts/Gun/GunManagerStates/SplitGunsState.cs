using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitGunsState : GunState
{

	[SerializeField] private Transform m_pullTargetLocation;
	
	[SerializeField] private float m_pushForce = 10;

	private Hittable m_pulledHittable ;

	private bool m_targetIsInPlace ;
	
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
			
			m_pulledHittable.transform.position = Vector3.MoveTowards(m_pulledHittable.transform.position, m_pullTargetLocation.position,   20*Time.deltaTime);
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

			m_pulledHittable = null;
			m_targetIsInPlace = false;
			
		}
	}

	public void ReleaseTarget()
	{
		if(m_pulledHittable.OnReleased != null) m_pulledHittable.OnReleased(m_pulledHittable);
		m_pulledHittable = null;
		m_targetIsInPlace = false;
	}

	public void PullTarget(Hittable t)
	{	
		m_pulledHittable = t;
		if (t.OnPulled != null) t.OnPulled(t);
	}
}
