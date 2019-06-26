using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EffectGroup : MonoBehaviour
{
	private IEffect[] m_effects;
	
	
	// Use this for initialization
	void Start ()
	{
		m_effects = GetComponentsInChildren<IEffect>();
	}
	
	public int GetEffectCount()
	{
		return m_effects.Length;
	}
	
	public IEffect GetEffectAt(int index)
	{
		return m_effects[index];
	}

	
	public void PlayAllEffects()
	{ 
		for (int i = 0; i < m_effects.Length; i++)
		{
			m_effects[i].Play();
		}
	}
	
	public void PlayAllEffectsInAColor(Color c)
	{
		for (int i = 0; i < m_effects.Length; i++)
		{
			m_effects[i].Play();
		}
	}

	public void PlayARandomEffects()
	{
		m_effects[Random.Range(0, m_effects.Length - 1)].Play();
	}
	

	public void PlayARandomEffectInAColor(Color c)
	{
		
		IEffect effects = m_effects[Random.Range(0, m_effects.Length - 1)];

		effects.PlayInAColor(c);
		
	}
	

}
