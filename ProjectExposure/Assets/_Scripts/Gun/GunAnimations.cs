﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GunAnimations : MonoBehaviour
{

	[SerializeField] private Animator m_animator;
	
	
	// Use this for initialization
	void Start () 
	{
		//TODO unsubscribe from these actions
		MergedGunsState.OnShoot += delegate(Hittable hittable, GunManager manager) {  m_animator.SetTrigger("ShootMerged");};
		MergedGunsState.OnMerge += delegate(MergedGunsState state) {m_animator.SetTrigger("Merge");  };
		SplitGunsState.OnSplit += delegate(SplitGunsState state) {m_animator.SetTrigger("UnMerge");  };
		SplitGunsState.OnShoot += delegate(Hittable hittable, GunManager manager, Gun gun) 
		{ 
			if(gun.gunSide == Gun.GunSide.RIGHT) m_animator.SetTrigger("ShootRight");
			else m_animator.SetTrigger("ShootLeft");
		 };
		UltimateState.OnUltimateEnter += delegate(UltimateState state) { m_animator.SetTrigger("UnMerge"); };

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private void OnDestroy()
	{
		
	}
}
