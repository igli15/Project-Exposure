using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SplitGunsState : GunState
{
	public static Action<Hittable,GunManager,Gun> OnShoot;
	public static Action<SplitGunsState> OnSplit;

	
	public enum GunMode
	{
		COLOR,
		SHOOT
	}

	[SerializeField] private Gun m_leftGun;
	[SerializeField] private Gun m_rightGun;
	
	private Gun m_currentGun;
	private GunMode m_currentMode;

	public GunMode currentMode
	{
		get { return m_currentMode; }
	}

	public override void Enter(IAgent pAgent)
	{
		if (OnSplit != null) OnSplit(this);
		base.Enter(pAgent);
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
	}

	public override void Shoot()
	{
		if (target.isMouseDown || EventSystem.current.IsPointerOverGameObject()) return; //if the mouse is clicking on the gun dont shoot!
		
		//Hittable hittable = target.RaycastFromGuns();

		Vector3 cameraViewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		if (cameraViewportPos.x > 0.5f) m_currentGun = m_rightGun;
		else m_currentGun = m_leftGun;
		
		Hittable hittable = m_currentGun.RaycastFromGuns();
		if(OnShoot != null) OnShoot(hittable, target,m_currentGun);
		
		if(hittable != null)
		{
			
			if (hittable.GetColor() == Color.white)
			{
				m_currentMode = GunMode.COLOR;
				
				hittable.Hit(target,0,target.color);
					
			}
			else
			{
				m_currentMode = GunMode.SHOOT;
				
				float m_damage = target.CalculateDamage(target.color, hittable.GetColor());
				hittable.Hit(target,m_damage,target.color);
			}
		}
	}

	public override void SetGunColor(Color c)
	{
		m_leftGun.color = c;
		m_rightGun.color = c;
	}
}
