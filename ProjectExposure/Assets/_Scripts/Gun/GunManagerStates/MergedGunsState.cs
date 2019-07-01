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

	private bool m_mergedBefore = false;

	private Tween m_mergeTimeTween;

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);
		
		m_mergedGun.manager = target;
		if (OnMerge != null) OnMerge(this);

		if (!m_mergedBefore)
		{
			VideoManager.instance.PlayVideo("rainbow");
			m_mergedBefore = true;
			m_mergeTimeTween = DOVirtual.DelayedCall(m_mergedTimeInSeconds + (float)VideoManager.instance.videoPlayer.clip.length, delegate { target.fsm.ChangeState<SplitGunsState>(); });
		}
		else m_mergeTimeTween = DOVirtual.DelayedCall(m_mergedTimeInSeconds, delegate { target.fsm.ChangeState<SplitGunsState>(); });
	}
	

	public override void Shoot(int touchIndex)
	{
		Hittable h = m_mergedGun.Shoot(touchIndex);
		if (OnShoot != null)  OnShoot(m_mergedGun,h);
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
