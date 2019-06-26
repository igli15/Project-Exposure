using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{			
	public  Action<Health> OnDeath;
	public  Action<Health> OnHealthIncreased;
	public  Action<Health> OnHealthDecreased;

	[SerializeField] 
	private float m_health = 100;

	[SerializeField] 
	private float m_maxHealth = 100;

	private float m_initialHealth;
	
	[SerializeField] 
	private bool m_shouldBeDestroyed;

    private bool m_canTakeDamage = true;
	
	void Start ()
	{
		m_initialHealth = m_health;
	}

	public void InflictDamage(float damageAmount)
	{
        if (m_canTakeDamage)
        {
            m_health -= damageAmount;

            if (OnHealthDecreased != null) OnHealthDecreased(this);
        }
	}
	
	public void HealUp(float healAmount)
	{
        m_health += healAmount;
        if (OnHealthIncreased != null) OnHealthIncreased(this);
	}

	public void ResetHealth()
	{
		m_health = m_initialHealth;
	}



    public float HP { get { return m_health; } }
	
	void Update () 
	{
		if (m_health <= 0)
		{
			if(OnDeath != null)	OnDeath(this);
			m_canTakeDamage = false;
			if (m_shouldBeDestroyed)
			{
				Destroy(gameObject);
			}
		
		}

		if (m_health > m_maxHealth)
		{
			m_health = m_maxHealth;
		}
	}

    public bool CanTakeDamage { get { return m_canTakeDamage; }set { m_canTakeDamage = value; } }

	public float MaxHealth
	{
		get { return m_maxHealth; }
	}

	public float GetCurrentHealth
	{
		get { return m_health; }
	}
}
