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

	private void SetShootTrigger(Hittable hittable, GunManager manager, Gun gun)
	{
		if(gun.gunSide == Gun.GunSide.RIGHT) m_animator.SetTrigger("ShootRight");
		else m_animator.SetTrigger("ShootLeft");
	}

	private void SetMergedShootTrigger(Hittable hittable, GunManager manager)
	{
		m_animator.SetTrigger("ShootMerged");
	}
}
