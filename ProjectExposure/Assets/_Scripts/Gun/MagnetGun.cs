using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagnetGun : Gun
{	
	[SerializeField] private GameObject m_rays;

	[SerializeField] private Transform m_pullTargetLocation;

	private List<Material> m_rayMats;

	private Hittable m_pulledHittable = null;

	private bool m_targetIsInPlace = false;

	private Tweener m_pullTween;
	public Hittable pulledHittable
	{
		get { return m_pulledHittable; }
		set { m_pulledHittable = value; }
	}

	public Transform pullTargetLocation
	{
		get { return m_pullTargetLocation; }
	}

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

	private void ShakeRays()
	{
		for (int i = 0; i < m_rays.transform.childCount; i++)
		{
			Sequence s = DOTween.Sequence();
			s.Append(m_rays.transform.GetChild(i).DOScaleZ(0.85f,0.25f));
			s.Append(m_rays.transform.GetChild(i).DOScaleZ(1, 0.25f));
		}
	}

	public override void SetColor(Color newColor)
	{
		base.SetColor(newColor);

		for (int i = 0; i < m_rayMats.Count; i++)
		{
			m_rayMats[i].color = newColor;
		}
	}

	public void PushTarget(Vector3 dir)
	{
		if (m_targetIsInPlace)
		{
			m_pulledHittable.GetComponent<Rigidbody>().AddForce(dir * 5, ForceMode.Impulse);
			
			if (m_pulledHittable.OnPushed != null) m_pulledHittable.OnPushed(m_pulledHittable);
			
			m_pulledHittable = null;
			m_targetIsInPlace = false;
			
		}
	}

	public void ReleaseTarget()
	{
		m_pullTween.Kill();
		
		if(pulledHittable.OnReleased != null) pulledHittable.OnReleased(pulledHittable);
	}

	public void PullTarget(Hittable t)
	{
		m_pullTween =  t.transform.DOMove(m_pullTargetLocation.position, 2.0f);
		m_pulledHittable = t;
		m_pullTween.onComplete += delegate { m_targetIsInPlace = true; };
		if (t.OnPulled != null) t.OnPulled(t);
	}
}
