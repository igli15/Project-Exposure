using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagnetGun : Gun 
{

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
	}

	protected override void HitAnHittable(Hittable hittable)
	{
		base.HitAnHittable(hittable);
		
		hittable.transform.DOMove(transform.position,2.0f);

	}


	protected override void Update()
	{
		base.Update();
	}

}
