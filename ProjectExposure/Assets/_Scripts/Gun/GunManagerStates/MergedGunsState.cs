using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class MergedGunsState : GunState
{
	
	public static Action<MergedGunsState> OnMerge;
	public static Action<Hittable, GunManager> OnShoot;

	[SerializeField] private MergedGun m_mergedGun;
	

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);

		if (OnMerge != null) OnMerge(this);
	}

	public override void Shoot()
	{
		if (target.isMouseDown) return; //if the mouse is clicking on the gun dont shoot!
		
		Hittable hittable = target.RaycastFromGuns();
		
		if (OnShoot != null) OnShoot(hittable,target);
		
		if(hittable != null)
		{
			hittable.Hit(target,100,target.color);
		}
	}

	public override void SetGunColor(Color c)
	{
		m_mergedGun.color = c;
	}
}
