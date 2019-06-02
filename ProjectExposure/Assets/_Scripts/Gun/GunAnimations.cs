using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GunAnimations : MonoBehaviour
{

	[SerializeField] private Animator m_animator;
	
	
	// Use this for initialization
	void Start () 
	{
		MergedGunsState.OnShoot += delegate(Hittable hittable, GunManager manager) {  m_animator.SetTrigger("ShootMerged");};
		MergedGunsState.OnMerge += delegate(MergedGunsState state) {m_animator.SetTrigger("Merge");  };
		SplitGunsState.OnSplit += delegate(SplitGunsState state) {m_animator.SetTrigger("UnMerge");  };
		UltimateState.OnUltimateEnter += delegate(UltimateState state) { m_animator.SetTrigger("UnMerge"); };

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
