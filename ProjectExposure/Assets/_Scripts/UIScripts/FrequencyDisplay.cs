using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrequencyDisplay : MonoBehaviour
{
	[SerializeField] private Image m_backgroundImage;
	[SerializeField] private Slider m_slider;

	[SerializeField] private Vector2 m_frequencyWaveLengthRange;
	
	private Material m_material;
	
	// Use this for initialization
	void Start ()
	{
		m_material = GetComponent<Image>().material;

		m_slider.onValueChanged.Invoke(m_slider.value);
		
		m_slider.onValueChanged.AddListener(delegate(float value)
		{
			float finalValue = Utils.Remap(value, 0, 1, 0.5f, 0.1f);
			
			m_material.SetFloat("_Wavelength", finalValue);
		});
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
