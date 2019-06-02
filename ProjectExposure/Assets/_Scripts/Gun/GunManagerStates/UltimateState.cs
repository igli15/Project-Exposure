using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateState : GunState 
{
	public override void Shoot()
	{
		if (target.isMouseDown) return; //if the mouse is clicking on the gun dont shoot!
		
		Hittable hittable = target.RaycastFromGuns();
		
		if(hittable != null)
		{
			
			if (hittable.GetColor() == Color.white)
			{
				hittable.Hit(target,0,target.color);
					
			}
			else
			{
				float m_damage = target.CalculateDamage(target.color, hittable.GetColor());
				hittable.Hit(target,m_damage,target.color);
			}
		}
	}
}
