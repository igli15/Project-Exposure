using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GunAnimations : MonoBehaviour
{

	[SerializeField] private Animator m_animator;
	
	
	// Use this for initialization
	void Start () 
	{
		MergedGunsState.OnShoot += SetMergedShootTrigger;
		MergedGunsState.OnMerge += SetMergeTrigger;
		SplitGunsState.OnSplit += SetUnMergeTrigger;
		SplitGunsState.OnShoot += SetShootTrigger;
	}

	private void SetMergeTrigger(MergedGunsState state)
	{
		m_animator.SetTrigger("Merge");  
	}
	
	private void SetUnMergeTrigger(SplitGunsState state)
	{
		m_animator.SetTrigger("UnMerge");  
	}

	private void SetShootTrigger(SingleGun singleGun,Hittable hittable)
	{
		if(singleGun.gunSide == SingleGun.GunSide.RIGHT) m_animator.SetTrigger("ShootRight");
		else m_animator.SetTrigger("ShootLeft");
	}

	private void SetMergedShootTrigger(AbstractGun manager,Hittable hittable)
	{
		m_animator.SetTrigger("ShootMerged");
	}

	private void OnDestroy()
	{
		MergedGunsState.OnShoot -= SetMergedShootTrigger;
		MergedGunsState.OnMerge -= SetMergeTrigger;
		SplitGunsState.OnSplit -= SetUnMergeTrigger;
		SplitGunsState.OnShoot -= SetShootTrigger;
	}
}
