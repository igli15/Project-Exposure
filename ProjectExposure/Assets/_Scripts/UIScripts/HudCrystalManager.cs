using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudCrystalManager : MonoBehaviour
{
	[SerializeField] private HudCrystal[] m_hudCrystals;

	private void Start()
	{
		SplitGunsState.OnSplit += ResetAllCrystals;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			ResetAllCrystals(null);
		}
	}

	public void ActivateCrystalAt(int index)
	{
		m_hudCrystals[index].ActivateCrystal();
	}

	public HudCrystal GetCrystalAt(int index)
	{
		return m_hudCrystals[index];
	}
	
	public void ResetAllCrystals(SplitGunsState splitGunsState)
	{
		foreach (HudCrystal crystal in m_hudCrystals)
		{
			crystal.ResetCrystalSprite();
		}
	}

	private void OnDestroy()
	{
		SplitGunsState.OnSplit -= ResetAllCrystals;
	}
}
