using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlaneEffects : MonoBehaviour,IEffect
{
	private MeshRenderer m_meshRenderer;
	
	private Renderer m_renderer;

	private Tween m_tween;

	[SerializeField] [Range(0.1f, 1.0f)] private float m_fadeTime = 0.2f;
	[SerializeField] [Range(0.1f, 4.0f)] private float m_colorIntensity = 1.5f;

	private void Start()
	{
		m_meshRenderer = GetComponent<MeshRenderer>();
		m_renderer = GetComponent<Renderer>();
		m_meshRenderer.enabled = false;
	}

	public void Play()
	{
		m_meshRenderer.enabled = true;
		
		m_tween.Kill();
		m_tween = m_renderer.material.DOFade(0, m_fadeTime);
		m_tween.onComplete += delegate { m_meshRenderer.enabled = false; };
	}

	public void PlayInAColor(Color c)
	{
		m_renderer.material.color = c;
		m_renderer.material.SetColor("_EmissionColor", c * m_colorIntensity);
		Play();
	}
}
