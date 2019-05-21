﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour,IAgent
{
	[SerializeField] private Gun m_damageGun;
	[SerializeField] private Gun m_colorGun;

	[SerializeField] private float m_baseDamage = 10;
	[SerializeField] private float m_extraDamage = 20;
	[SerializeField] private float m_hueDamageRange = 40;
	
	private float m_damage = 0;

	private Fsm<GunManager> m_fsm;
	private Renderer m_mergeSphereRenderer;

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

	public float damage
	{
		get { return m_damage; }
	}
	
	void Start () 
	{
		SetGunColors(Color.red);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ShootTheRightGun();
		}
	}

	public void ShootTheRightGun()
	{
		List<Hittable> hittables = RaycastFromGuns();

		foreach (Hittable h in hittables)
		{
			if (h.GetColor() == Color.white)
			{
				m_currentMode = GunMode.COLOR;
				m_colorGun.Shoot();
				h.Hit(this,0,m_colorGun.GetColor());
					
			}
			else
			{
				m_currentMode = GunMode.SHOOT;
				m_damageGun.Shoot();
				m_damage = CalculateDamage(m_damageGun.GetColor(), h.GetColor());
				h.Hit(this,m_damage,m_damageGun.GetColor());
			}
		}
	}
	
	public void SetGunColors(Color newColor)
	{
		m_damageGun.SetColor(newColor);
		m_colorGun.SetColor(newColor);
	}
	
	public float CalculateDamage(Color myColor,Color enemyColor)
	{
		float damage = 0;

		if (ColorUtils.GetHSVOfAColor(enemyColor).y < 0.2f)
		{
			Debug.Log(ColorUtils.GetHSVOfAColor(enemyColor).y);
			return 0;
		}
		
		float enemyHue = ColorUtils.GetHueOfColor(enemyColor) * 360;

		float hue = ColorUtils.GetHueOfColor(myColor) * 360;
        
		float hueDiff = Mathf.Abs(enemyHue - hue);

		if (hueDiff <= m_hueDamageRange)
		{
			float precisionLevel= ((m_hueDamageRange - hueDiff) / m_hueDamageRange);
			damage =  m_baseDamage + precisionLevel * m_extraDamage;
		}
		Debug.Log(damage);
		return damage;
	}

	
	protected List<Hittable> RaycastFromGuns()
	{        
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
		m_colorGun.LookInRayDirection(ray);
		m_damageGun.LookInRayDirection(ray);
		
		RaycastHit[] hits;
		
		List<Hittable> hittables = new List<Hittable>();
		
		if(EventSystem.current.IsPointerOverGameObject()) return hittables;
		
		hits = Physics.RaycastAll(ray);

		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			Hittable hittable = hit.transform.gameObject.GetComponent<Hittable>();
            
			if(hittable != null)
			{
				hittables.Add(hittable);
			}
		}

		return hittables;
	}
}
