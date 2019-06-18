using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class SingleGun : AbstractGun
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

	public override Hittable Shoot(int touchIndex)
	{
        //Debug.Log(touchIndex);
		Hittable hittable = RaycastFromGuns(touchIndex);
		
		
		if(hittable != null)
		{
			
			if (hittable.GetColor() == Color.white)
			{
				//m_currentMode = GunMode.COLOR;
				
				hittable.Hit(this,0);
					
			}
			else
			{
				//m_currentMode = GunMode.SHOOT;
				
				float m_damage = manager.CalculateDamage(color, hittable.GetColor());
				hittable.Hit(this,m_damage);
			}
		}

		return hittable;
	}
}
