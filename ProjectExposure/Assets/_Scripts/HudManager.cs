using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HudManager : MonoBehaviour,IAgent
{
	private Fsm<HudManager> m_fsm;

	[SerializeField] private GameObject m_mergeButton;
	[SerializeField] private GameObject m_mergeLines;

	private Vector3 m_leftBorderInit;
	private Vector3 m_rightBorderInit;
	private Vector3 m_sliderInit;
	
	void Start () 
	{
		if (m_fsm == null)
		{
			m_fsm = new Fsm<HudManager>(this);
		}
		
		
		//SetUpElements();
		
		m_fsm.ChangeState<SplitHudState>();

		m_mergeButton.SetActive(false);
		
		MergedGunsState.OnMerge += ChangeStateToMerged;
		SplitGunsState.OnSplit += ChangeStateToSplit;
		SplitGunsState.OnColorsCollected += EnableMergeButton;

	}

	public void ChangeStateToMerged(MergedGunsState mergedGunsState)
	{
		m_fsm.ChangeState<MergedHudState>();
	}
	
	public void ChangeStateToSplit(SplitGunsState splitGunsState)
	{
		m_fsm.ChangeState<SplitHudState>();
	}
	
	
	public void EnableMergeButton(SplitGunsState state = null)
	{
		m_mergeLines.SetActive(true);
		m_mergeButton.SetActive(true);
	}
	
	public void DisableMergeButton()
	{
		m_mergeLines.SetActive(false);
		m_mergeButton.SetActive(false);
	}

	private void OnDestroy()
	{
		MergedGunsState.OnMerge -= ChangeStateToMerged;
		SplitGunsState.OnSplit -= ChangeStateToSplit;
		SplitGunsState.OnColorsCollected -= EnableMergeButton;
	}
}
