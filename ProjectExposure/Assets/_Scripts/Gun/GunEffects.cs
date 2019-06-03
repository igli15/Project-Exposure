using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class GunEffects : MonoBehaviour 
{

	[Header("Beam")]
	[SerializeField] private LineRenderer m_lineRenderer;

	[SerializeField] private Material m_magnetBeam;
	[SerializeField] private Material m_shootBeam;

	[Range(0.1f, 2)] [SerializeField] private float m_colorIntensity = 2;
	[Range(0, 1)] [SerializeField] private float m_beamFadeDuration = 0.3f;
	[Range(0, 1)] [SerializeField] private float m_saturationScale = 0.5f;
	[Range(0, 1)] [SerializeField] private float m_valueScale = 1;

	[Header("Particles")]
	[SerializeField] private ParticleSystem m_particleFeedback;

	[Header("Muzzle Flash")]
	[SerializeField] private GameObject m_muzzleFlashGameObject;

	[Range(0, 1)] [SerializeField] private float m_alpha = 0.3f;
	[Range(0, 0.1f)] [SerializeField] private float m_fadeDuration = 0.1f;

	
	private Material m_muzzleFlashMaterial;
	
	private Renderer m_beamRenderer;

	private Color m_muzzleColor;
	
	private void Awake()
	{
		m_beamRenderer = m_lineRenderer.GetComponent<Renderer>();
		m_muzzleFlashMaterial = m_muzzleFlashGameObject.GetComponent<Renderer>().material;

		m_muzzleColor = m_muzzleFlashMaterial.GetColor("_TintColor");
		m_muzzleColor.a = 0;
		m_muzzleFlashMaterial.SetColor("_TintColor",m_muzzleColor);
		
		
	}

	public void InitMergeBeam(Hittable hittable, GunManager manager)
	{
		if (manager.isMouseDown) return;

		Color c = manager.color;

		Vector3 hsv = ColorUtils.GetHSVOfAColor(c);
		hsv.y *= m_saturationScale;
		hsv.z *= m_valueScale;
		c = Color.HSVToRGB(hsv.x, hsv.y, hsv.z, true);
		c *= m_colorIntensity;
		c.a = 1;

		Material mat = m_shootBeam;

		m_beamRenderer.material = mat;
		mat.SetColor("_TintColor", c);

		SetLineRendererPoints(hittable,manager);

		c = mat.GetColor("_TintColor");
		DOVirtual.Float(c.a, 0, m_beamFadeDuration,
			(delegate(float value)
			{
				c.a = value;
				m_lineRenderer.SetPosition(0, m_lineRenderer.transform.position);
				mat.SetColor("_TintColor", c);
			}));
	}

	public void UseMagneticBeam(Hittable hittable,GunManager manager)
	{
		Color c = hittable.GetColor();
		Vector3 hsv = ColorUtils.GetHSVOfAColor(c);
		hsv.y *= m_saturationScale;
		hsv.z *= m_valueScale;
		c = Color.HSVToRGB(hsv.x,hsv.y,hsv.z,true);
		c *= m_colorIntensity;
		c.a = 1;
		
		Material mat = m_magnetBeam;

		m_beamRenderer.material = mat;
		mat.SetColor("_TintColor", c);
			
		SetLineRendererPoints(hittable,manager);

	}
	
	public void FadeMagneticBeam(Hittable hittable,GunManager manager)
	{
		Color c = m_beamRenderer.material.GetColor("_TintColor");
		DOVirtual.Float(c.a, 0, m_beamFadeDuration,
			(delegate(float value)
			{
				c.a = value;
				m_lineRenderer.SetPosition(0, m_lineRenderer.transform.position);
				m_beamRenderer.material.SetColor("_TintColor", c);
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
	
	}

	private void SetLineRendererPoints(Hittable hittable,GunManager manager)
	{
		m_lineRenderer.SetPosition(0,m_lineRenderer.transform.position);
		if(hittable != null) m_lineRenderer.SetPosition(1,hittable.transform.position);
		else  m_lineRenderer.SetPosition(1,manager.origin.position + manager.GetDirFromGunToMouse() * 40);
	}
}
