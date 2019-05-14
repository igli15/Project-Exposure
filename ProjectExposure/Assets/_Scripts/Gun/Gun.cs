using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour,IAgent
{
	public Action<Gun> OnShoot;

	public Action<Gun> OnColorChanged;
	
	protected Color m_color;

	protected Material m_material;

	protected Fsm<Gun> m_fsm;
	
	// Use this for initialization
	protected virtual void Start ()
	{
		m_material = GetComponent<Renderer>().material;

		if (m_fsm == null)
		{
			m_fsm = new Fsm<Gun>(this);
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if (Input.GetMouseButton(0))
		{
			Shoot();
		}
	}

	protected void Shoot()
	{
		if(EventSystem.current.IsPointerOverGameObject()) return;
        
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
		//if(m_fsm.GetCurrentState() is SeperatedGunState)
		LookInRayDirection(ray);
      
		RaycastHit[] hits;
		hits = Physics.RaycastAll(ray);

		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			Hittable hittable = hit.transform.gameObject.GetComponent<Hittable>();
            
			if(hittable != null)
			{
				hittable.HitByGun(this);
			}
		}
	}

	private void LookInRayDirection(Ray ray)
	{
		Ray r = ray;
		r.origin = transform.position;
		Quaternion rot = Quaternion.LookRotation(r.direction.normalized,Vector3.up);
		Sequence s = DOTween.Sequence();
		Tween t = transform.DORotate(rot.eulerAngles, 0.5f);
	}

	public Color GetColor()
	{
		return m_color;
	}

	public virtual void SetColor(Color newColor)
	{
		m_color = newColor;
		m_material.SetColor("_Color", newColor);
		if (OnColorChanged != null) OnColorChanged(this);
	}
}
