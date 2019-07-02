using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	[SerializeField] private Crystal m_redCrystal;
	[SerializeField] private Crystal m_greenCrystal;
	[SerializeField] private Crystal m_blueCrystal;

	[SerializeField] private Image m_hintImage;
	[SerializeField] private Image m_highlightImage;
	[SerializeField] private HintPanel m_hintPanel;
	
	[SerializeField] private Sprite[] m_hintSprites;
	[SerializeField] private Sprite[] m_highlightSprites;

    [SerializeField] private CompanionButton m_companionButton;
    [SerializeField] private Transform m_tutorialFinger;


	// Use this for initialization
	void Start ()
	{
		m_highlightImage.sprite = m_highlightSprites[0];
		m_hintImage.sprite = m_hintSprites[0];
		
		
		m_redCrystal.OnExplode += GreenHints;
		m_greenCrystal.OnExplode += BlueHints;
		m_blueCrystal.OnExplode += CompleteTutorial;

        m_companionButton.ShowTutorialHint();

        MoveFinger();
	}

	void GreenHints(Crystal c)
	{
		m_highlightImage.sprite = m_highlightSprites[1];
		m_hintImage.sprite = m_hintSprites[1];
	}
	
	void BlueHints(Crystal c)
	{
		m_highlightImage.sprite = m_highlightSprites[2];
		m_hintImage.sprite = m_hintSprites[2];
	}

	void CompleteTutorial(Crystal c)
	{
		m_hintPanel.Hide();
		gameObject.SetActive(false);
	}

	void MoveFinger()
	{
		Sequence s = DOTween.Sequence();

		s.Append(m_tutorialFinger.DOMove(m_tutorialFinger.GetChild(1).position, 1));
		s.Append(m_tutorialFinger.DOMove(m_tutorialFinger.GetChild(2).position, 1));
		s.Append(m_tutorialFinger.DOMove(m_tutorialFinger.GetChild(1).position, 1));
		s.Append(DOVirtual.DelayedCall(0.1f, delegate { m_tutorialFinger.gameObject.SetActive(false); }));
	}
}
