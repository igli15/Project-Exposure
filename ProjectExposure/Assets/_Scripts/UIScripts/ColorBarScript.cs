using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ColorBarScript : MonoBehaviour
{
	[SerializeField] 
	private float m_pointerRange = 20;
	
	private PointerScript[] m_pointers;
	
	// Use this for initialization
	void Start ()
	{
		m_pointers = GetComponentsInChildren<PointerScript>();

		for (int i = 0; i < m_pointers.Length; i++)
		{
			m_pointers[i].OnPointerUpdated += IncreaseGunDamage;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	bool CheckIfPointersAreNear()
	{
		if (Mathf.Abs(m_pointers[0].GetComponent<RectTransform>().anchoredPosition.x -
		    m_pointers[1].GetComponent<RectTransform>().anchoredPosition.x) <= m_pointerRange)
		{
			return true;
		}

		return false;
	}

	void IncreaseGunDamage(PointerScript pointer)
	{
		if (CheckIfPointersAreNear())
		{
			for (int i = 0; i < m_pointers.Length; i++)
			{
				m_pointers[i].GetGun().SetAoe(true);
			}
		}
		else
		{
			for (int i = 0; i < m_pointers.Length; i++)
			{
				m_pointers[i].GetGun().SetAoe(false);
			}
		}
	}
}
