using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeButton : TouchButton
{

	[SerializeField] private GunManager m_gunManager;

	private bool merged = false;
	// Use this for initialization
	void Start ()
	{
		OnTouchEnter.AddListener(delegate
		{
			if (merged)
			{
				merged = false;
				m_gunManager.SplitGuns();
			}
			else
			{
				merged = true;
				m_gunManager.MergeGuns();
			}
		});
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
