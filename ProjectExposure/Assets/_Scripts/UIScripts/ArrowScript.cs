using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowScript : TouchButton
{

	enum ArrowType
	{
		LEFT,
		RIGHT
	}
	
	[SerializeField] 
	private float m_vlaueIncreaseSpeed = 1.0f;

	[SerializeField] 
	private Slider m_slider;
	
	[SerializeField] 
	private ArrowType m_arrowType;
	

	// Use this for initialization
	void Start () 
	{
		OnTouch.AddListener(UpdateSliders);
	}
	
	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

	void UpdateSliders()
	{
		if (m_arrowType == ArrowType.RIGHT)
		{
			m_slider.value -= m_vlaueIncreaseSpeed * Time.deltaTime;
		}
		else
		{
			m_slider.value += m_vlaueIncreaseSpeed * Time.deltaTime;
		}
	}

	
	/*
	bool IsMouseOver()
	{
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = Input.mousePosition;

		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData,results);
		GameObject obj = null;
		for (int i = 0; i < results.Count; i++)
		{
			if (results[i].gameObject.GetInstanceID() == gameObject.GetInstanceID())
			{
				obj = results[i].gameObject;
			}
		}

		if (obj != null && obj.GetInstanceID() == this.gameObject.GetInstanceID())
		{
			return true;
		}
		else return false;
	}
*/



}
