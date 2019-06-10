using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedGun : AbstractGun 
{
	public override void Shoot()
	{
		//if (target.isMouseDown) return; //if the mouse is clicking on the gun dont shoot!
		
		Hittable hittable = RaycastFromGuns();
		
		//if (OnShoot != null) OnShoot(hittable,target);
		
		if(hittable != null)
		{
			//hittable.Hit(target,100,target.color);
		}
	}
}
