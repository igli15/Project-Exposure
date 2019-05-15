using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitGunsState : AbstractState<GunManager> {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			target.ShootTheRightGun();
		}
	}
}
