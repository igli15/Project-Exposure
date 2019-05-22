using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBase : MonoBehaviour
{

	public Action<float> OnSliderValueChanged;
	
	[SerializeField] private Transform m_minTransform;

	[SerializeField] private Transform m_maxTransform;
	
	[SerializeField] private SliderHandle m_sliderHandle;

	[Range(0,1)]
	[SerializeField]
	private float m_sliderValue = 0;

	public float sliderValue
	{
		get { return m_sliderValue; }
	}


	// Use this for initialization
	void Start ()
	{
		m_sliderHandle.OnSliderDragged += SliderDragged;
	}

	public void SliderDragged(SliderHandle handle)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hitInfo = Physics.RaycastAll(ray,20);
		
		for (int i = 0; i < hitInfo.Length; i++)
		{
			if (hitInfo[i].collider.transform.CompareTag("SliderBase") )
			{	
				m_sliderValue = (hitInfo[i].point.z - m_minTransform.position.z) /
				          (m_maxTransform.position.z - m_minTransform.position.z);

				m_sliderValue = Mathf.Clamp(m_sliderValue, 0, 1);

				if (OnSliderValueChanged != null) OnSliderValueChanged(m_sliderValue);
				
				handle.transform.position =
					Vector3.Lerp(m_minTransform.position, m_maxTransform.position, m_sliderValue);

			}
		}
	}
}
