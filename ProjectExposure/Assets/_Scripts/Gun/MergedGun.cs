using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedGun : AbstractGun
{
	[SerializeField] private Transform m_cannonTransform;
	
	public override Hittable Shoot(int touchIndex)
	{
		Hittable hittable = RaycastFromGuns(touchIndex);
		LookInRayDirection(m_cannonTransform, new Ray(m_cannonTransform.position, GetDirFromGunToTouch()));
		
		if(hittable != null)
		{
			hittable.Hit(this,100);
		}

		return hittable;
	}

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
