using System;
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
	[SerializeField] private float m_mergedTimeInSeconds = 10;
	

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);

		m_mergedGun.manager = target;
		if (OnMerge != null) OnMerge(this);

		DOVirtual.DelayedCall(m_mergedTimeInSeconds, delegate { target.fsm.ChangeState<SplitGunsState>(); });
	}

	public override void Shoot(int touchIndex)
	{
		Hittable h = m_mergedGun.Shoot(touchIndex);
		if (OnShoot != null)  OnShoot(m_mergedGun,h);
	}

	public override void SetGunColor(Color c)
	{
		m_mergedGun.color = c;
	}
}
