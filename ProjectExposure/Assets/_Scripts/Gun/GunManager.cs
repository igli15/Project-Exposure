using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class GunManager : MonoBehaviour,IAgent
{
	public static Action<Color> OnColorChanged;
	
	[SerializeField] private AbstractGun[] guns;
	
	[SerializeField] private float m_baseDamage = 10;
	[SerializeField] private float m_extraDamage = 20;
	[SerializeField] private float m_hueDamageRange = 40;
	
	[SerializeField] private Transform m_origin;

	[SerializeField] private RectTransform m_shootingArea;
	
	private float m_damage = 0;

	private Fsm<GunManager> m_fsm;

	private bool m_mouseDown = false;

	private Color m_color;

	public Fsm<GunManager> fsm
	{
		get { return m_fsm; }
	}

	public float damage
	{
		get { return m_damage; }
	}

	public Color color
	{
		get { return m_color; }
	}

	public Transform origin
	{
		get { return m_origin; }
	}

	public bool isMouseDown
	{
		get { return m_mouseDown; }
	}

	public RectTransform shootingArea
	{
		get { return m_shootingArea; }
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
	
	public void SetGunColors(Color newColor)
	{
		if (OnColorChanged != null) OnColorChanged(newColor);
		
		foreach (AbstractGun gun in guns)
		{
			gun.color = newColor;
		}
		//m_color = newColor;
	}
	
	public float CalculateDamage(Color myColor,Color enemyColor)
	{
		float damage = 0;

		if (ColorUtils.GetHSVOfAColor(enemyColor).y < 0.2f)
		{
			return 0;
		}
		
		float enemyHue = ColorUtils.GetHueOfColor(enemyColor) * 360;

		float hue = ColorUtils.GetHueOfColor(myColor) * 360;
        
		float hueDiff = Mathf.Abs(enemyHue - hue);

		if (hueDiff <= m_hueDamageRange)
		{
			float precisionLevel= ((m_hueDamageRange - hueDiff) / m_hueDamageRange);
			damage =  m_baseDamage + precisionLevel * m_extraDamage;
		}
		//Debug.Log(damage);
		return damage;
	}

	public int GetGunCount()
	{
		return guns.Length;
	}

	public AbstractGun GetGunAt(int index)
	{
		return guns[index];
	}

	private void OnMouseDown()
	{
		m_mouseDown = true;
	}

	private void OnMouseUp()
	{
		m_mouseDown = false;
	}
}
