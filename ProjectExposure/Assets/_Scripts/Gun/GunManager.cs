﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour,IAgent
{
	public Action<GunManager> OnMerge;
	public Action<GunManager> OnSplit;

	[SerializeField] private MagnetGun m_magnetGun;
	[SerializeField] private ColorGun m_colorGun;

	[SerializeField] private GameObject m_mergeSphere;

	[SerializeField] private float m_baseDamage = 10;
	[SerializeField] private float m_extraDamage = 20;
	[SerializeField] private float m_hueDamageRange = 40;
	
	private float m_damage = 0;

	private Fsm<GunManager> m_fsm;
	private Renderer m_mergeSphereRenderer;

	public enum GunMode
	{
		COLOR,
		MAGNET,
		MERGED
	};

	private GunMode m_currentMode;
	
	public MagnetGun magnetGun
	{
		get { return m_magnetGun; }
	}
	public ColorGun colorGun
	{
		get { return m_colorGun; }
	}

	public GameObject mergeSphere
	{
		get { return m_mergeSphere; }
	}

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
		if (m_fsm == null)
		{
			m_fsm = new Fsm<GunManager>(this);
		}
		
		m_mergeSphereRenderer = m_mergeSphere.GetComponent<Renderer>();
		SetGunColors(Color.red);
		
		m_fsm.ChangeState<MergedGunsState>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void ShootTheRightGun()
	{	
		if(EventSystem.current.IsPointerOverGameObject()) return;
        
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
		
		if (magnetGun.pulledHittable != null)
		{
			magnetGun.PushTarget(m_magnetGun.LookInRayDirection(ray));
			return;
		}
		
		m_colorGun.LookInRayDirection(ray);
		m_magnetGun.LookInRayDirection(ray);
		
		RaycastHit[] hits;
		hits = Physics.RaycastAll(ray);

		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			Hittable hittable = hit.transform.gameObject.GetComponent<Hittable>();
            
			if(hittable != null)
			{
				Debug.Log("crystal color: "+hittable.GetColor() + " " + "magnet color: " + m_magnetGun.GetColor() );
				Debug.Log(hittable.GetColor() == m_magnetGun.GetColor());
				if (CheckIfColorAreSimilar(magnetGun.GetColor(), hittable.GetColor(), 30)) 
				{ 
					m_currentMode = GunMode.MAGNET;
					hittable.Hit(this);
				}
				else if (hittable.GetColor() == Color.white && m_magnetGun.pulledHittable == null)
				{
					m_currentMode = GunMode.COLOR;
					hittable.Hit(this);
					
				}
				
			}
		}
	}

	public void ShootMergedGun()
	{
		if(EventSystem.current.IsPointerOverGameObject()) return;
        
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit[] hits;
		hits = Physics.RaycastAll(ray);

		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			Hittable hittable = hit.transform.gameObject.GetComponent<Hittable>();
            
			if(hittable != null)
			{
				m_currentMode = GunMode.MERGED;
				m_damage = CalculateDamage(m_colorGun.GetColor(),hittable.GetColor());
				hittable.Hit(this);
			}
		}
	}

	
	
	public void SetGunColors(Color newColor)
	{
		m_magnetGun.SetColor(newColor);
		m_colorGun.SetColor(newColor);
	
		m_mergeSphereRenderer.material.color = newColor;
	}

	public void MergeGuns()
	{
		m_fsm.ChangeState<MergedGunsState>();

		if (m_magnetGun.pulledHittable != null)
		{
			m_magnetGun.ReleaseTarget();
		}
		
		if(OnMerge != null) OnMerge(this);
	}

	public void SplitGuns()
	{
		m_fsm.ChangeState<SplitGunsState>();
		
		SetGunColors(Color.red);
		
		if(OnSplit != null) OnSplit(this);
	}
	
	public float CalculateDamage(Color myColor,Color enemyColor)
	{
		float damage = 0;
		float enemyHue = colorGun.GetHueOfColor(enemyColor) * 360;

		float hue = colorGun.GetHueOfColor(myColor) * 360;
        
		float hueDiff = Mathf.Abs(enemyHue - hue);

		if (hueDiff <= m_hueDamageRange)
		{
			float precisionLevel= ((m_hueDamageRange - hueDiff) / m_hueDamageRange);
			damage =  m_baseDamage + precisionLevel * m_extraDamage;
		}
		Debug.Log(damage);
		return damage;
	}

	public bool CheckIfColorAreSimilar(Color c1, Color c2,float range)
	{
		
		float c1Hue = colorGun.GetHueOfColor(c1) * 360;

		float c2Hue = colorGun.GetHueOfColor(c2) * 360;

		if (c1Hue > 340) c1Hue = 0;
		if (c2Hue > 340) c1Hue = 0;  //Reset red.
		
		float hueDiff = Mathf.Abs(c1Hue - c2Hue);

		if (hueDiff <= range)
		{
			return true;
		}

		return false;
	}

}
