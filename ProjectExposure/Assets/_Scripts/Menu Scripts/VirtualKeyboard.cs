using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class VirtualKeyboard : MonoBehaviour
{
	public UnityEvent OnShow;
	public UnityEvent OnHide;
	public UnityEvent OnSave;
	
	private string m_finalString;
	private TMP_InputField m_inputField;
	private CanvasGroup m_canvasGroup;
	private bool m_isShown = false;
	
	[Space(30)]
	[SerializeField] private KeyboardButton m_saveButton;
	[SerializeField] private KeyboardButton m_backSpaceButton;

	[SerializeField] [Range(0, 1)] private float m_appearTime = 0.5f;
	[SerializeField] [Range(0, 1)] private float m_dissapearTime = 0.5f;

	public TMP_InputField inputField
	{
		get { return m_inputField; }
	}

	private void Start()
	{
		m_saveButton.isSaveButton = true;
		m_saveButton.OnClick.AddListener(delegate { Apply(); });
		m_backSpaceButton.OnClick.AddListener(delegate {m_inputField.text =  m_inputField.text.Remove(m_inputField.text.Length - 1); });
	}

	public string finalString
	{
		set {m_finalString = value;}
		get { return m_finalString; }
	}

	private void Update()
	{
		/*
		if (Input.GetMouseButtonDown(0) && m_isShown)
		{
			Vector2 m_mouseViewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			if (m_panelImage.rectTransform.anchorMax.y < m_mouseViewPos.y)
			{
				HideKeyboard(true);
			}
		}
		*/
	}

	public void ShowKeyboard(TMP_InputField i)
	{
		OnShow.Invoke();
		
		DOVirtual.DelayedCall(0.1f, delegate { m_isShown = true; });
		
		m_inputField = i;
	}
	
	
	public void HideKeyboard(bool discard = false)
	{
		OnHide.Invoke();

		if (discard) m_inputField.text = "";
		
		DOVirtual.DelayedCall(0.1f, delegate { m_isShown = false; });

		m_inputField = null;
		m_finalString = "";
	}

	public void Apply()
	{
		HighScoreManager.instance.LoadHighScores();
		HighScoreManager.instance.SubmitHighScore(m_inputField.text);
		
		OnSave.Invoke();
		
		HideKeyboard(false);

	}
}
