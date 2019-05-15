using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour,IAgent
{

	public Action<GunManager> OnMerge;
	public Action<GunManager> OnSplit;

	[SerializeField] private MagnetGun m_magnetGun;
	[SerializeField] private ColorGun m_colorGun;

	[SerializeField] private GameObject m_mergeSphere;

	private Fsm<GunManager> m_fsm;

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

	void Start () 
	{
		if (m_fsm == null)
		{
			m_fsm = new Fsm<GunManager>(this);
		}
		
		m_fsm.ChangeState<SplitGunsState>();
		SetGunColors(Color.red);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void ShootTheRightGun()
	{
		if(EventSystem.current.IsPointerOverGameObject()) return;
        
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
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
				else if((hittable.GetColor() == m_magnetGun.GetColor()))
				{
					m_magnetGun.Shoot();
				}
			}
		}
	}

	public void SetGunColors(Color newColor)
	{
		m_magnetGun.SetColor(newColor);
		m_colorGun.SetColor(newColor);
	}

	public void MergeGuns()
	{
		m_fsm.ChangeState<MergedGunsState>();
		if(OnMerge != null) OnMerge(this);
	}

	public void SplitGuns()
	{
		m_fsm.ChangeState<SplitGunsState>();
		if(OnSplit != null) OnSplit(this);
	}
}
