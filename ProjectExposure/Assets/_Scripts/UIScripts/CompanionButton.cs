using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CompanionButton : HudButton
{
	[SerializeField] private HintPanel m_hintPanel;

	private void Start()
	{
		OnTouchEnter.AddListener(delegate {
			m_hintPanel.Show(); });
		
		ShowButton("Gate1Hint");
		m_hintPanel.gameObject.SetActive(true);
		m_hintPanel.Show();
		m_hintPanel.canHide = false;
	}

	public void ShowButton(string h)
	{
		m_hintPanel.hintName = h;
		FillButton(true);
	}
}
