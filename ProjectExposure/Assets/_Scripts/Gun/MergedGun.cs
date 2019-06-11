using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedGun : AbstractGun
{
	[SerializeField] private Transform m_cannonTransform;
	
	public override Hittable Shoot()
	{
		Hittable hittable = RaycastFromGuns();
		LookInRayDirection(m_cannonTransform, new Ray(m_cannonTransform.position, GetDirFromGunToMouse()));
		
		if(hittable != null)
		{
			hittable.Hit(this,100);
		}

		return hittable;
	}
}
