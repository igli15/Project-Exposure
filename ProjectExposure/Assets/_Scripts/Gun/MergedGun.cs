using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedGun : AbstractGun 
{
	public override Hittable Shoot()
	{
		Hittable hittable = RaycastFromGuns();

		if(hittable != null)
		{
			hittable.Hit(this,100);
		}

		return hittable;
	}
}
