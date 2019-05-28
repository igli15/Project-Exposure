using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GunEffects : MonoBehaviour 
{

	[Header("Beam")]
	[SerializeField] private LineRenderer m_lineRenderer;

	[Range(0.1f, 2)] [SerializeField] private float m_colorIntensity = 2;
	[Range(0, 1)] [SerializeField] private float m_beamFadeDuration = 0.3f;

	[Header("Particles")]
	[SerializeField] private ParticleSystem m_particleFeedback;

	[Header("Muzzle Flash")]
	[SerializeField] private GameObject m_muzzleFlashGameObject;

	[Range(0, 1)] [SerializeField] private float m_alpha = 0.3f;
	[Range(0, 0.1f)] [SerializeField] private float m_fadeDuration = 0.1f;

	
	private Material m_muzzleFlashMaterial;
	private Material m_beamMat;

	private Color m_muzzleColor;
	
	private void Awake()
	{
		m_beamMat = m_lineRenderer.GetComponent<Renderer>().material;
		m_muzzleFlashMaterial = m_muzzleFlashGameObject.GetComponent<Renderer>().material;

		m_muzzleColor = m_muzzleFlashMaterial.GetColor("_TintColor");
		m_muzzleColor.a = 0;
		m_muzzleFlashMaterial.SetColor("_TintColor",m_muzzleColor);
		
		MergedGunsState.OnShoot += InitBeam;
		MergedGunsState.OnShoot += PlayParticles;
		MergedGunsState.OnShoot += EnableMuzzleFlash;
		
	}

	public void InitBeam(Hittable hittable,GunManager manager)
	{
		if (manager.isMouseDown) return;
		
		Color c = manager.color;
		c *= m_colorIntensity;
		c.a = 1;
		m_beamMat.SetColor("_TintColor", c);
			
		m_lineRenderer.SetPosition(0,m_lineRenderer.transform.position);
		Debug.DrawRay(manager.origin.position,manager.GetDirFromGunToMouse() * 50,Color.red);
		if(hittable != null) m_lineRenderer.SetPosition(1,hittable.transform.position);
		else  m_lineRenderer.SetPosition(1,manager.origin.position + manager.GetDirFromGunToMouse() * 40);

		c = m_beamMat.GetColor("_TintColor");
		DOVirtual.Float(c.a, 0, m_beamFadeDuration, 
			(delegate(float value) { c.a = value;
				m_lineRenderer.SetPosition(0,m_lineRenderer.transform.position);
				m_beamMat.SetColor("_TintColor", c);
			}));
	}

	public void PlayParticles(Hittable hittable, GunManager manager)
	{
		if (manager.isMouseDown) return;
		
		ParticleSystem.MainModule settingsFeedback = m_particleFeedback.main;
		settingsFeedback.startColor = new ParticleSystem.MinMaxGradient( manager.color );
		
		m_particleFeedback.Play();
	}

	public void EnableMuzzleFlash(Hittable hittable, GunManager manager)
	{
		if (manager.isMouseDown) return;
		
		Sequence s = DOTween.Sequence();

		m_muzzleColor = manager.color;
		m_muzzleColor.a = 0;
		
		s.Append(DOVirtual.Float(m_muzzleColor.a,m_alpha,m_fadeDuration,(delegate(float value)
		{
			m_muzzleColor.a = value;
			m_muzzleFlashMaterial.SetColor("_TintColor",m_muzzleColor);
		})));
		
		s.Append(DOVirtual.Float(m_muzzleColor.a,0f,m_fadeDuration,(delegate(float value)
		{
			m_muzzleColor.a = value;
			m_muzzleFlashMaterial.SetColor("_TintColor",m_muzzleColor);
		})));
		
	}

	private void OnDestroy()
	{
		MergedGunsState.OnShoot -= InitBeam;
		MergedGunsState.OnShoot -= PlayParticles;
	}
}
