using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour,IAgent
{
	private Fsm<HudManager> m_fsm;

	[SerializeField] private HudButton m_mergeButton;
	[SerializeField] private CompanionButton m_companionButton;
	[SerializeField] private Image m_rainbowImage;

	private Vector3 m_leftBorderInit;
	private Vector3 m_rightBorderInit;
	private Vector3 m_sliderInit;
	
	void Start () 
	{
		if (m_fsm == null)
		{
			m_fsm = new Fsm<HudManager>(this);
		}
		
		
		m_fsm.ChangeState<SplitHudState>();
		
		
		MergedGunsState.OnMerge += ChangeStateToMerged;
		SplitGunsState.OnSplit += ChangeStateToSplit;
		SplitGunsState.OnColorsCollected += EnableMergeButton;

		m_mergeButton.gameObject.SetActive(false);
		
		m_companionButton.ShowButton("Gate1Hint");
	}

	private void Update()
	{
		
		if (Input.GetKeyDown(KeyCode.K))
		{
			m_companionButton.ShowButton("Gate1Hint");
		}
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
		m_mergeButton.gameObject.SetActive(true);
		m_mergeButton.FillButton();
		Tween t = m_rainbowImage.transform.DOScaleX(1, 0.3f);
	}
	
	public void DisableMergeButton()
	{
		m_mergeButton.UnFillButton();
		Tween t = m_rainbowImage.transform.DOScaleX(0, 0.3f);
		t.onComplete += delegate { m_mergeButton.gameObject.SetActive(false); };
	}

	private void OnDestroy()
	{
		MergedGunsState.OnMerge -= ChangeStateToMerged;
		SplitGunsState.OnSplit -= ChangeStateToSplit;
		SplitGunsState.OnColorsCollected -= EnableMergeButton;
	}
}
