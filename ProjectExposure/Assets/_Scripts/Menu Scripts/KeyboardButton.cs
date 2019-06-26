using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyboardButton : MonoBehaviour,IPointerDownHandler
{
	public UnityEvent OnClick;
	
	[SerializeField] private string m_key = "";

	private Image m_image;
	private Tween m_delayTween;

	private VirtualKeyboard m_parentKeyboard;

	private bool m_isSaveButton = false;
	public string key
	{
		get { return m_key; }
	}

	public bool isSaveButton
	{
		get { return m_isSaveButton; }
		set { m_isSaveButton = value; }
	}

	private void Start()
	{
		m_parentKeyboard = GetComponentInParent<VirtualKeyboard>();
		m_image = GetComponent<Image>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnClick.Invoke();

		if (isSaveButton) return;
		
		m_delayTween.Kill();
	
		m_image.color = Color.gray;

		m_delayTween = DOVirtual.DelayedCall(0.2f, delegate { m_image.color = Color.white; }, true);
		//Debug.Log( m_key );

		m_parentKeyboard.finalString += m_key;
		m_parentKeyboard.inputField.text += m_key;
	}
}
