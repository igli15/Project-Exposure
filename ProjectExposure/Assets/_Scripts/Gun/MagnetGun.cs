﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagnetGun : Gun
{	
	[SerializeField] private GameObject m_rays;
	
	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		
		InvokeRepeating("ShakeRays",0.5f,0.6f);
	}

	protected override void HitAnHittable(Hittable hittable)
	{
		base.HitAnHittable(hittable);
		
		hittable.transform.DOMove(transform.position,2.0f);

	}

	private void ShakeRays()
	{
		for (int i = 0; i < m_rays.transform.childCount; i++)
		{
			Sequence s = DOTween.Sequence();
			s.Append(m_rays.transform.GetChild(i).DOScaleZ(0.85f,0.25f));
			s.Append(m_rays.transform.GetChild(i).DOScaleZ(1, 0.25f));
		}
	}

	protected override void Update()
	{
		base.Update();
	}

	
}
