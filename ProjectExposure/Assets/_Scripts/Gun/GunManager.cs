using System;
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
		SHOOT,
	}

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
		m_mergeSphereRenderer = m_mergeSphere.GetComponent<Renderer>();
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
				h.Hit(this,0);
					
			}
			else
			{
				m_currentMode = GunMode.SHOOT;
				m_damage = CalculateDamage(m_colorGun.GetColor(), h.GetColor());
				h.Hit(this,m_damage);
			}
		}
	}
	
	public void SetGunColors(Color newColor)
	{
		m_magnetGun.SetColor(newColor);
		m_colorGun.SetColor(newColor);
	
		m_mergeSphereRenderer.material.color = newColor;
	}
	
	public float CalculateDamage(Color myColor,Color enemyColor)
	{
		float damage = 0;

		if (colorGun.GetHSVOfAColor(enemyColor).y < 0.2f)
		{
			Debug.Log(colorGun.GetHSVOfAColor(enemyColor).y);
			return 0;
		}
		
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

	
	protected List<Hittable> RaycastFromGuns()
	{        
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
		m_colorGun.LookInRayDirection(ray);
		m_magnetGun.LookInRayDirection(ray);
		
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
