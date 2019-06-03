using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour
{
	[SerializeField] private GameObject m_mergeButton;
	

	// Use this for initialization
	void Start () 
	{
		//SplitGunsState.OnPull += delegate(Hittable hittable, GunManager manager) { m_mergeButton.SetActive(false); };
		
		//SplitGunsState.OnPush += delegate(Hittable hittable, GunManager manager) { m_mergeButton.SetActive(true); };
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
