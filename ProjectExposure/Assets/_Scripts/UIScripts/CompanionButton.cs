using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CompanionButton : HudButton
{
	[SerializeField] private HintPanel m_hintPanel;
	[SerializeField] private Animator m_companionAppearEffect;

	private void Start()
	{
		OnTouchEnter.AddListener(delegate {
			m_hintPanel.Show(); });

        UnFillButton();
	}

	public void ShowButton(string h)
	{
		m_hintPanel.hintName = h;
		
		m_companionAppearEffect.gameObject.SetActive(true);
		
		DOVirtual.DelayedCall(m_companionAppearEffect.runtimeAnimatorController.animationClips[0].length, delegate { m_companionAppearEffect.gameObject.SetActive(false); });
		
		FillButton(true);
	}

    public void ShowTutorialHint()
    {
        ShowButton("Gate1Hint");
        m_hintPanel.gameObject.SetActive(true);
        m_hintPanel.Show();
        m_hintPanel.canHide = false;
    }
}
