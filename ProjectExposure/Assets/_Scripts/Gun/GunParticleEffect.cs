using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParticleEffect : MonoBehaviour,IEffect
{
	private ParticleSystem m_particleSystem;
	
	[SerializeField] [Range(0.1f, 2.0f)] private float m_colorIntensity = 1.5f;
	
	private void Start()
	{
		m_particleSystem = GetComponent<ParticleSystem>();
	}

	public void Play()
	{
		m_particleSystem.Play();
	}

	public void PlayInAColor(Color c)
	{
		ParticleSystem.MainModule settingsFeedback = m_particleSystem.main;
		settingsFeedback.startColor = new ParticleSystem.MinMaxGradient( c );
		Renderer r = m_particleSystem.GetComponent<Renderer>();
		r.material.SetColor("_EmissionColor",c * m_colorIntensity);
		m_particleSystem.Play();
	}
}
