using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HudCrystal : MonoBehaviour
{
	[SerializeField] private Color m_color;
	[SerializeField] private Sprite m_disableSprite;
	
	private Sprite m_initSprite;
	private Animator m_animator;
	private Image m_image;

	private bool m_isActive = false;

	public bool isActive
	{
		get { return m_isActive; }
	}

	public Color color
	{
		get { return m_color; }
	}

	// Use this for initialization
	void Awake ()
	{
		m_image = GetComponent<Image>();
		
		m_initSprite = m_image.sprite;
		
		m_animator = GetComponent<Animator>();
	}

	public void ActivateCrystal()
	{
		if(!m_isActive)m_animator.SetTrigger("Activate");
		m_isActive = true;
	}

	public void ChangeToDisableSprite()
	{
		m_image.sprite = m_disableSprite;
		
		
	}

	public void FadeCrystal()
	{
		if(m_isActive) m_animator.SetTrigger("DeActivate");
		m_isActive = false;
		m_image.DOFade(0, 0.2f);
		
	}

	public void ResetCrystalSprite()
	{
		m_image.DOFade(1, 0);
		m_image.sprite = m_initSprite;
	}
}
