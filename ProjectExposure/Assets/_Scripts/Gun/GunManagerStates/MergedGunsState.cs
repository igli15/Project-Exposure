using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MergedGunsState : AbstractState<GunManager>
{

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);

		target.colorGun.transform.DOLookAt(target.mergeSphere.transform.position,0.5f);
		target.magnetGun.transform.DOLookAt(target.mergeSphere.transform.position,0.5f);
	}

	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			target.ShootMergedGun();
		}
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
		
		target.colorGun.transform.DORotate(new Vector3(0,0,0), 0.5f);
		target.magnetGun.transform.DORotate(new Vector3(0,0,0), 0.5f);
	}
}
