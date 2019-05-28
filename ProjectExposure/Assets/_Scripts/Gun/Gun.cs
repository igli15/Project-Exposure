using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Gun : MonoBehaviour
{
	public Action<Gun> OnShoot;
	
	public virtual void Shoot()
	{
		if (OnShoot != null) OnShoot(this);
	}
}
