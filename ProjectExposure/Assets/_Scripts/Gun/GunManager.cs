using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour {

	// Use this for initialization

	[SerializeField] private MagnetGun m_magnetGun;
	[SerializeField] private ColorGun m_colorGun;
	
	
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ShootTheRightGun();
		}
	}

	private void ShootTheRightGun()
	{
		if(EventSystem.current.IsPointerOverGameObject()) return;
        
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
		//if(m_fsm.GetCurrentState() is SeperatedGunState)
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
				if (hittable.GetColor() == Color.white)
				{
					m_colorGun.Shoot();
				}
				else if((hittable.GetColor() == m_colorGun.GetColor()))
				{
					m_magnetGun.Shoot();
				}
			}
		}
	}
}
