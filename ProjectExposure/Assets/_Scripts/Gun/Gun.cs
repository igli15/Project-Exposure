using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : AbstractGun
{
	public enum GunSide
	{
		LEFT,
		RIGHT
	}

	[SerializeField] private GunSide m_gunSide;

	public GunSide gunSide
	{
		get { return m_gunSide; }
	}

	public override void Shoot()
	{
		
	}
}
