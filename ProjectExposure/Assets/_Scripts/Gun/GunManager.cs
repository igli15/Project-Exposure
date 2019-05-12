using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{


	[SerializeField] 
	private Gun[] m_guns;

	[SerializeField] 
	private float m_aoeRange = 20;

	[SerializeField]
	private GameObject mergeSphere;
	
	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < m_guns.Length; i++)
		{
			m_guns[i].OnChargeChanged += CheckForAOE;
			m_guns[i].OnHueChanged += MixColorOfGuns;
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


	public void MixColorOfGuns(Gun gun)
	{

		Color mixedColor = MixColors(m_guns[0].GetComponent<Renderer>().material.GetColor("_Color"),
			m_guns[1].GetComponent<Renderer>().material.GetColor("_Color"));
		
		mergeSphere.GetComponent<Renderer>().material.SetColor("_Color",mixedColor);
		//Debug.Log(mixedColor);
	}
	Color MixColors(Color c1, Color c2)
	{
		Color mixture = Color.black;
		
		mixture.r = (c1.r + c2.r) / 2;
		mixture.g = (c1.g + c2.g) / 2;
		mixture.b = (c1.b + c2.b) / 2;
		
		return mixture;
	}

	public GameObject mergeSpehre
	{
		get { return mergeSphere; }
	}
}
