using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{


	[SerializeField] 
	private Gun[] m_guns;

	[SerializeField] 
	private float m_aoeRange = 20;
	
	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < m_guns.Length; i++)
		{
			m_guns[i].OnChargeChanged += CheckForAOE;
		}
	}

	void CheckForAOE(Gun gun)
	{
		for (int i = 0; i < m_guns.Length; i++)
		{
			if (m_guns[i].GetInstanceID() !=gun.gameObject.GetInstanceID())
			{
				if (Mathf.Abs(gun.Hue()  - m_guns[i].Hue()) <= m_aoeRange)
				{
					gun.SetAoe(true);
					m_guns[i].SetAoe(true);
				}
			}
		}
	}
	
}
