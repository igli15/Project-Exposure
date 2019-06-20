using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionButton : HudButton
{
	[SerializeField] private HintPanel m_hintPanel;
	
	private void Start()
	{
		OnTouch.AddListener(delegate {
			m_hintPanel.Show("Gate3Hint"); });
	}
}
