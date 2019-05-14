using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Gun : MonoBehaviour,IAgent
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

	protected virtual void HitAnHittable(Hittable hittable)
	{
		if (hittable.OnHit != null) hittable.OnHit(hittable);
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		
	}

	public void Shoot()
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
				HitAnHittable(hittable);
			}
		}
	}

	public void LookInRayDirection(Ray ray)
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
		
		if (OnColorChanged != null) OnColorChanged(this);
	}
}
