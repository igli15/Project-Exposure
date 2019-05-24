using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour,IAgent
{
	[SerializeField] private Transform m_gunGroup;

	[SerializeField] private float m_baseDamage = 10;
	[SerializeField] private float m_extraDamage = 20;
	[SerializeField] private float m_hueDamageRange = 40;
	
	private float m_damage = 0;

	private Fsm<GunManager> m_fsm;

	private bool m_mouseDown = false;

	private Color m_color;

	public Fsm<GunManager> fsm
	{
		get { return m_fsm; }
	}

	public float damage
	{
		get { return m_damage; }
	}

	public Color color
	{
		get { return m_color; }
	}
	
	void Start () 
	{
		if (m_fsm == null)
		{
			m_fsm = new Fsm<GunManager>(this);
		}
		
		m_fsm.ChangeState<MergedGunsState>();
		
		SetGunColors(Color.red);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public void SetGunColors(Color newColor)
	{
		m_color = newColor;
	}
	
	public float CalculateDamage(Color myColor,Color enemyColor)
	{
		float damage = 0;

		if (ColorUtils.GetHSVOfAColor(enemyColor).y < 0.2f)
		{
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

	
	public List<Hittable> RaycastFromGuns()
	{        
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
     		
		RaycastHit[] hits;
		
		List<Hittable> hittables = new List<Hittable>();
		
		if(EventSystem.current.IsPointerOverGameObject()) return hittables;

		//LookInRayDirection(m_colorGun.transform, ray);
		//LookInRayDirection(m_damageGun.transform, ray);
		
		if(!m_mouseDown)
		LookInRayDirection(m_gunGroup, ray);
		
		
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
	
	public Vector3 LookInRayDirection(Transform t,Ray ray)
	{
		Ray r = ray;
		r.origin = transform.position;
		Quaternion rot = Quaternion.LookRotation(r.direction.normalized,Vector3.up);
		t.DORotate(rot.eulerAngles, 0.5f);
		return r.direction;
	}

	private void OnMouseDown()
	{
		m_mouseDown = true;
	}

	private void OnMouseUp()
	{
		m_mouseDown = false;
	}
}
