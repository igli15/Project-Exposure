using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.Experimental.UIElements.Slider;

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

    [SerializeField] private UnityEngine.UI.Slider m_slider;

    private bool m_moveFinger = true;

    private Sequence m_fingerSequence;

	// Use this for initialization
	void Start ()
	{
		m_fingerSequence = DOTween.Sequence();
		m_highlightImage.sprite = m_highlightSprites[0];
		m_hintImage.sprite = m_hintSprites[0];
		
		
		m_redCrystal.OnExplode += GreenHints;
		m_greenCrystal.OnExplode += BlueHints;
		m_blueCrystal.OnExplode += CompleteTutorial;

        m_companionButton.ShowTutorialHint();

        MoveFinger();
        m_slider.onValueChanged.AddListener(delegate(float arg0)
        {
	        m_moveFinger = false; 
	        m_fingerSequence.Kill();
	        m_tutorialFinger.gameObject.SetActive(false);
        });
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
		m_fingerSequence.Append(m_tutorialFinger.DOMove(m_tutorialFinger.GetChild(1).position, 1));

		for (int i = 0; i <= 20; i++)
		{
			m_fingerSequence.Append(m_tutorialFinger.DOMove(m_tutorialFinger.GetChild(1).position, 1));
			m_fingerSequence.Append(m_tutorialFinger.DOMove(m_tutorialFinger.GetChild(2).position, 1));
		}


	}

	
}
