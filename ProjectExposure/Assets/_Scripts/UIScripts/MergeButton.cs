using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeButton : TouchButton
{

	[SerializeField] private GunManager m_gunManager;

	// Use this for initialization
	void Start ()
	{
		
		OnTouchEnter.AddListener(delegate
		{
			if (m_gunManager.fsm.GetCurrentState() is MergedGunsState)
			{
				
				m_gunManager.fsm.ChangeState<SplitGunsState>();
			}
			else
			{
				m_gunManager.fsm.ChangeState<MergedGunsState>();
			}
		});
		
	}
	
}
