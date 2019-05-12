using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MergedGunState : AbstractState<Gun>
{
	private Transform m_mergeSphere;
	private Vector3 m_oldPos;

	// Use this for initialization
	void Start ()
	{
		
		
	}

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);
		
		target.SetAoe(true);
		
		
		m_mergeSphere = transform.parent.GetComponent<GunManager>().mergeSpehre.transform;
		
		Vector3 pos = m_mergeSphere.position;
		
		target.transform.DOLookAt(pos , 0.5f);
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
		
		target.SetAoe(false);
		
		target.transform.DORotate(new Vector3(0,0,0), 0.5f);

	}
}
