using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ResoScreenEffects : MonoBehaviour
{
	
	[SerializeField] private Transform m_buttonGroup;
	[SerializeField] private Transform m_mainMenuPanel;
	[SerializeField] private Transform m_leaderBoardPanel;

	private Vector3 m_mainMenuInitScale;
	private Vector3 m_leaderboardInitScale;

	private void Start()
	{
		
		Setup();
	}

	public void MoveButtonsDown()
	{
		Sequence s = DOTween.Sequence();
		
		s.Append(m_buttonGroup.DOMove(Vector3.zero, 1f).SetEase(Ease.Linear));

		s.Append(m_mainMenuPanel.DOScale(m_mainMenuInitScale,0.5f));
		s.Append(m_leaderBoardPanel.DOScale(m_leaderboardInitScale,0.5f));

	}


	private void Setup()
	{
		m_mainMenuInitScale = m_mainMenuPanel.transform.localScale;
		m_leaderboardInitScale = m_leaderBoardPanel.transform.localScale;
		m_mainMenuPanel.localScale = new Vector3(m_mainMenuPanel.localScale.x,0,m_mainMenuPanel.localScale.z);
		m_leaderBoardPanel.localScale = new Vector3(m_leaderBoardPanel.localScale.x,0,m_leaderBoardPanel.localScale.z);
	}
}
