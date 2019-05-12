using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MergedGunState : AbstractState<Gun>
{
	private Transform mergeSphere;

	// Use this for initialization
	void Start ()
	{
		
	}

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);
		mergeSphere = transform.parent.GetComponent<GunManager>().mergeSpehre.transform;
		Vector3 pos = mergeSphere.position;
		
		target.transform.DOLookAt(2 * transform.position - pos, 0.5f);
	}

	// Update is called once per frame
	void Update () 
	{
		
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
	}
}
