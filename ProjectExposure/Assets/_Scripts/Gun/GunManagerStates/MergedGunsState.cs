﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class MergedGunsState : GunState
{
	
	public static Action<MergedGunsState> OnMerge;
	public static Action<MergedGun,Hittable> OnShoot;

	[SerializeField] private MergedGun m_mergedGun;
	

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);

		m_mergedGun.manager = target;
		if (OnMerge != null) OnMerge(this);
	}

	public override void Shoot()
	{
		Hittable h = m_mergedGun.Shoot();
		if (OnShoot != null)  OnShoot(m_mergedGun,h);
	}

	public override void SetGunColor(Color c)
	{
		m_mergedGun.color = c;
	}
}
