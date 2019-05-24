using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MergedGunsState : AbstractState<GunManager>
{
	public enum GunMode
	{
		COLOR,
		SHOOT,
	}
	
	private GunMode m_currentMode;
	
	public GunMode currentMode
	{
		get { return m_currentMode; }
	}

	public override void Enter(IAgent pAgent)
	{
		base.Enter(pAgent);
	}
	
	public void ShootTheRightGun()
	{
		List<Hittable> hittables = target.RaycastFromGuns();

		foreach (Hittable h in hittables)
		{
			if (h.GetColor() == Color.white)
			{
				m_currentMode = GunMode.COLOR;
				//m_colorGun.Shoot();
				h.Hit(target,0,target.color);
					
			}
			else
			{
				m_currentMode = GunMode.SHOOT;
				//m_damageGun.Shoot();
				float m_damage = target.CalculateDamage(target.color, h.GetColor());
				h.Hit(target,m_damage,target.color);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			ShootTheRightGun();
		}
	}

	public override void Exit(IAgent pAgent)
	{
		base.Exit(pAgent);
	}
}
