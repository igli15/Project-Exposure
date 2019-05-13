using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeButton : TouchButton
{
	[SerializeField] private Gun leftGun;
	[SerializeField] private Gun rightGun;


	private bool merged = false;
	// Use this for initialization
	void Start ()
	{
		OnTouchEnter.AddListener(delegate
		{
			if (merged)
			{
				merged = false;
				leftGun.fsm.ChangeState<SeperatedGunState>();
				rightGun.fsm.ChangeState<SeperatedGunState>();
			}
			else
			{
				merged = true;
				leftGun.fsm.ChangeState<MergedGunState>();
				rightGun.fsm.ChangeState<MergedGunState>();
			}
		});
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
