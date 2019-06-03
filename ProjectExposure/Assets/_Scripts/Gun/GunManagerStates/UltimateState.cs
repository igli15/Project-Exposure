using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateState : GunState
{

	public static Action<UltimateState> OnUltimateEnter;

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);

		if (OnUltimateEnter != null) OnUltimateEnter(this);
	}

	public override void Shoot()
	{
		if (target.isMouseDown) return; //if the mouse is clicking on the gun dont shoot!
		
		Hittable hittable = target.RaycastFromGuns();
		
		if(hittable != null)
		{
			hittable.Hit(target,100,target.color);
		}
	}
}
