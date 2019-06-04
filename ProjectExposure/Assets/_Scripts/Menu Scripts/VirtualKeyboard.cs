using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class VirtualKeyboard : MonoBehaviour
{
	public UnityEvent OnShow;
	public UnityEvent OnHide;
	public UnityEvent OnSave;
	
	private string m_finalString;
	private InputField m_inputField;
	private CanvasGroup m_canvasGroup;
	
	[Space(30)]
	[SerializeField] private KeyboardButton m_saveButton;

	[SerializeField] [Range(0, 1)] private float m_appearTime = 0.5f;
	[SerializeField] [Range(0, 1)] private float m_dissapearTime = 0.5f;

	public InputField inputField
	{
		get { return m_inputField; }
	}

	private void Start()
	{
		m_canvasGroup = GetComponent<CanvasGroup>();
		m_saveButton.OnClick.AddListener(delegate { OnSave.Invoke(); });
	}

	public string finalString
	{
		set {m_finalString = value;}
		get { return m_finalString; }
	}


	public void ShowKeyboard(InputField i)
	{
		OnShow.Invoke();
		m_canvasGroup.DOFade(1, m_appearTime);
		m_inputField = i;
	}
	
	
	public void HideKeyboard()
	{
		OnHide.Invoke();
		m_canvasGroup.DOFade(0, m_dissapearTime);
		m_inputField = null;
	}
}
