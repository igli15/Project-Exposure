using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PollStats : MonoBehaviour
{
	[HideInInspector] public List<int> slider1Values = new List<int>();
	[HideInInspector] public List<int>  slider2Values = new List<int>();

	[SerializeField] private DiscreteSlider m_slider1;
	[SerializeField] private DiscreteSlider m_slider2;

	private void Start()
	{
		SaveLoadScript.Load(this,"PollValues");
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			slider1Values.Clear();
			slider2Values.Clear();
			SaveLoadScript.Save(this, "PollValues");
		}
	}

	public void SubmitPoll()
	{
		slider1Values.Add(m_slider1.value);
		slider2Values.Add(m_slider2.value);
		SaveLoadScript.Save(this,"PollValues");
	}
}
