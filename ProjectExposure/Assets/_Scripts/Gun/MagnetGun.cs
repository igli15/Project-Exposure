using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagnetGun : Gun
{	
	[SerializeField] private GameObject m_rays;

	private List<Material> m_rayMats;

	private Transform m_pulledTransform = null;

	private void Awake()
	{
		m_rayMats = new List<Material>();
			
		for (int i = 0; i < 3; i++)
		{
			m_rayMats.Add(m_rays.transform.GetChild(i).GetComponent<Renderer>().material);
		}
	}

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		
		InvokeRepeating("ShakeRays",0.5f,0.6f);		
		
	}

	protected override void HitAnHittable(Hittable hittable)
	{
		base.HitAnHittable(hittable);

		if (m_pulledTransform == null)
		{
			hittable.transform.DOMove(transform.position, 2.0f);
			m_pulledTransform = hittable.transform;
		}
	}

	private void ShakeRays()
	{
		for (int i = 0; i < m_rays.transform.childCount; i++)
		{
			Sequence s = DOTween.Sequence();
			s.Append(m_rays.transform.GetChild(i).DOScaleZ(0.85f,0.25f));
			s.Append(m_rays.transform.GetChild(i).DOScaleZ(1, 0.25f));
		}
	}

	protected override void Update()
	{
		base.Update();
	}


	public override void SetColor(Color newColor)
	{
		base.SetColor(newColor);

		for (int i = 0; i < m_rayMats.Count; i++)
		{
			m_rayMats[i].color = newColor;
		}
	}
}
