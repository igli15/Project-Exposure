using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Hittable {

	// Use this for initialization
	void Start () 
	{
		SetColor(color);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	public override void Hit(GunManager gunManager)
	{
		if (OnHit != null) OnHit(this);
		
		if (gunManager.currentMode == GunManager.GunMode.COLOR)
		{
			SetColor(gunManager.colorGun.GetColor());
		}
		else if (gunManager.currentMode == GunManager.GunMode.MAGNET && gunManager.magnetGun.pulledHittable == null)
		{
			gunManager.magnetGun.PullTarget(this);
		}
		else if (gunManager.currentMode == GunManager.GunMode.MERGED && gunManager.colorGun.GetColor() == color)
		{
			Destroy(transform.gameObject);
		}
		
	}
}
