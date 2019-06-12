using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HudCrystalManager : MonoBehaviour
{
	[SerializeField] private HudCrystal[] m_hudCrystals;

	private void Start()
	{
		SplitGunsState.OnSplit += ResetAllCrystals;
	}

	public void ActivateCrystalAt(int index)
	{
		m_hudCrystals[index].ActivateCrystal();
	}

	public HudCrystal GetCrystalAt(int index)
	{
		return m_hudCrystals[index];
	}

	public void StartFadingCrystals()
	{
		Sequence s = DOTween.Sequence();

		for (int i = 0; i < m_hudCrystals.Length ; i++)
		{
			var i1 = i;

			Tween t = DOVirtual.DelayedCall(10f / 7.2f, delegate { m_hudCrystals[i1].FadeCrystal(); });
			t.onPlay += delegate { m_hudCrystals[i1].ChangeToDisableSprite(); };
			s.Append(t);
		}
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
