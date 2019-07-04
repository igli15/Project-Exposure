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

	[SerializeField] private Transform m_gunGroupTransform;

	private bool m_mergedBefore = false;

	private Tween m_mergeTimeTween;

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);
		
		m_mergedGun.manager = target;
		if (OnMerge != null) OnMerge(this);
		
		m_gunGroupTransform.DOLocalMove(new Vector3(m_gunGroupTransform.localPosition.x,m_gunGroupTransform.localPosition.y - 0.068f,m_gunGroupTransform.localPosition.z - 0.04f),0.5f);

		if (!PlayerStats.instance.hasMerged)
		{
			VideoManager.instance.PlayVideo("rainbow");
			PlayerStats.instance.hasMerged = true;
			m_mergeTimeTween = DOVirtual.DelayedCall(m_mergedTimeInSeconds + (float)VideoManager.instance.GetVideo("rainbow").videoPlayer.clip.length + 0.2f, delegate { target.fsm.ChangeState<SplitGunsState>(); });
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

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
		m_gunGroupTransform.DOLocalMove(new Vector3(m_gunGroupTransform.localPosition.x,m_gunGroupTransform.localPosition.y + 0.068f,m_gunGroupTransform.localPosition.z + 0.04f),0.5f);
	}
}
