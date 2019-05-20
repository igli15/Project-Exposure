using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBase : MonoBehaviour 
{	
	[SerializeField] private Transform m_minTransform;

	[SerializeField] private Transform m_maxTransform;
	
	[SerializeField] private SliderHandle m_sliderHandle;

	[Range(0,1)]
	public float m_sliderValue = 0;
	
	
	// Use this for initialization
	void Start ()
	{
		m_sliderHandle.OnSliderDragged += SliderDragged;
	}

	public void SliderDragged(SliderHandle handle)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hitInfo = Physics.RaycastAll(ray,10);
		
		for (int i = 0; i < hitInfo.Length; i++)
		{
			if (hitInfo[i].transform.CompareTag("SliderBase") )
			{	
				m_sliderValue = (hitInfo[i].point.z - m_minTransform.position.z) /
				          (m_maxTransform.position.z - m_minTransform.position.z);

				m_sliderHandle.transform.position = Vector3.Lerp(m_minTransform.position, m_maxTransform.position, m_sliderValue);
				
			}
		}
	}
}
