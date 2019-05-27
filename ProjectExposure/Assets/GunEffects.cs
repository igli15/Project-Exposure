using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GunEffects : MonoBehaviour 
{

	[SerializeField] private LineRenderer m_lineRenderer;
	[SerializeField] private Material m_beamMat;

	[SerializeField] private ParticleSystem m_particleFeedback;
	[SerializeField] private ParticleSystem m_particleSparks;

	private void Awake()
	{
		MergedGunsState.OnShoot += InitBeam;
		MergedGunsState.OnShoot += PlayParticles;
	}

	public void InitBeam(Hittable hittable,GunManager manager)
	{
		Color c = manager.color;
		c *= 2;
		c.a = 1;
		m_beamMat.SetColor("_TintColor", c);
			
		m_lineRenderer.SetPosition(0,m_lineRenderer.transform.position);
		Debug.DrawRay(manager.origin.position,manager.GetDirFromGunToMouse() * 40,Color.red);
		if(hittable != null) m_lineRenderer.SetPosition(1,hittable.transform.position);
		else  m_lineRenderer.SetPosition(1,manager.origin.position + manager.GetDirFromGunToMouse() * 40);

		c = m_beamMat.GetColor("_TintColor");
		DOVirtual.Float(c.a, 0, 0.5f, 
			(delegate(float value) { c.a = value;
				m_lineRenderer.SetPosition(0,m_lineRenderer.transform.position);
				m_beamMat.SetColor("_TintColor", c);
			}));
	}

	public void PlayParticles(Hittable hittable, GunManager manager)
	{
		ParticleSystem.MainModule settingsFeedback = m_particleFeedback.main;
		settingsFeedback.startColor = new ParticleSystem.MinMaxGradient( manager.color );
		
		ParticleSystem.MainModule settingsSparks = m_particleSparks.main;
		settingsSparks.startColor = new ParticleSystem.MinMaxGradient( manager.color );
		
		m_particleFeedback.Play();
		m_particleSparks.Play();
	}

	private void OnDestroy()
	{
		MergedGunsState.OnShoot -= InitBeam;
		MergedGunsState.OnShoot -= PlayParticles;
	}
}
