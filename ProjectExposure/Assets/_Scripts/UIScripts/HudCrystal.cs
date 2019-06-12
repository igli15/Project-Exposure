using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudCrystal : MonoBehaviour
{
	[SerializeField] private Color m_color;
	
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
		m_isActive = true;
		m_animator.SetTrigger("Activate");
	}

	public void ResetCrystalSprite()
	{
		m_isActive = false;
		m_animator.SetTrigger("DeActivate");
		m_image.sprite = m_initSprite;
	}
}
