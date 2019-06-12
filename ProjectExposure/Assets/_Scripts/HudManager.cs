using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HudManager : MonoBehaviour,IAgent
{
	private Fsm<HudManager> m_fsm;
	
	[SerializeField] private RectTransform m_leftBorder;
	[SerializeField] private RectTransform m_rightBorder;
	[SerializeField] private RectTransform m_slider;
	
	[SerializeField] private GameObject m_mergeButton;

	private Vector3 m_leftBorderInit;
	private Vector3 m_rightBorderInit;
	private Vector3 m_sliderInit;
	
	void Start () 
	{
		if (m_fsm == null)
		{
			m_fsm = new Fsm<HudManager>(this);
		}

		m_leftBorderInit = m_leftBorder.position;
		m_rightBorderInit = m_rightBorder.position;
		m_sliderInit = m_slider.position;
		
		//SetUpElements();
		
		m_fsm.ChangeState<SplitHudState>();

		m_mergeButton.SetActive(false);
		
		MergedGunsState.OnMerge += ChangeStateToMerged;
		SplitGunsState.OnSplit += ChangeStateToSplit;
		SplitGunsState.OnColorsCollected += EnableMergeButton;

	}

	private void  SetUpElements()
	{
		m_leftBorder.Translate(Vector3.left * (m_leftBorder.position.x - 800));
		m_rightBorder.Translate(Vector3.right * (m_rightBorder.position.x + 800));

		m_slider.Translate(Vector3.up * (m_slider.position.y - 600));
	}

	public void MoveElementsOutsideCanvas()
	{
		Sequence sequence = DOTween.Sequence();
		
		sequence.Append(m_leftBorder.DOMoveX(m_leftBorder.position.x - 800, 0.5f));
		m_rightBorder.DOMoveX(m_rightBorder.position.x + 800, 0.5f);

		sequence.Append(m_slider.DOMoveY(m_slider.position.y - 600, 0.5f));
	}
	
	public void MoveElementsInsideCanvas()
	{
		Sequence sequence = DOTween.Sequence();
		
		sequence.Append(m_leftBorder.DOMove(m_leftBorderInit, 0.5f));
		m_rightBorder.DOMove(m_rightBorderInit, 0.5f);

		sequence.Append(m_slider.DOMove(m_sliderInit, 0.5f));
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
		m_mergeButton.SetActive(true);
	}
	
	public void DisableMergeButton()
	{
		m_mergeButton.SetActive(false);
	}

	private void OnDestroy()
	{
		MergedGunsState.OnMerge -= ChangeStateToMerged;
		SplitGunsState.OnSplit -= ChangeStateToSplit;
		SplitGunsState.OnColorsCollected -= EnableMergeButton;
	}
}
