using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
	protected bool m_mouseEntered = false;

	public UnityEvent OnTouchEnter;
	public UnityEvent OnTouch;
	public UnityEvent OnTouchExit;
	
	protected virtual void Update () 
	{
		if (m_mouseEntered)
		{
			OnTouch.Invoke();
		}
	}
	
	public void OnPointerDown(PointerEventData eventData)
	{
		OnTouchEnter.Invoke();
		m_mouseEntered = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		OnTouchExit.Invoke();
		m_mouseEntered = false;
	}
}
