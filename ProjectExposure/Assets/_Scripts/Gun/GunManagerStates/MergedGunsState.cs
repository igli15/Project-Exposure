using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class MergedGunsState : GunState
{
	public static Action<Hittable,GunManager> OnShoot;
	public enum GunMode
	{
		COLOR,
		SHOOT,
	}

	[SerializeField] private Gun m_colorGun;
	[SerializeField] private Gun m_damageGun;
	
	private GunMode m_currentMode;

	public GunMode currentMode
	{
		get { return m_currentMode; }
	}


	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
	}

	public override void Shoot()
	{
		Hittable hittable = target.RaycastFromGuns();

		if(OnShoot != null) OnShoot(hittable, target);
		
		
		if(hittable != null)
		{
			
			if (hittable.GetColor() == Color.white)
			{
				m_currentMode = GunMode.COLOR;
				m_colorGun.Shoot();
				hittable.Hit(target,0,target.color);
					
			}
			else
			{
				m_currentMode = GunMode.SHOOT;
				m_damageGun.Shoot();
				float m_damage = target.CalculateDamage(target.color, hittable.GetColor());
				hittable.Hit(target,m_damage,target.color);
			}
		}
	}
}
