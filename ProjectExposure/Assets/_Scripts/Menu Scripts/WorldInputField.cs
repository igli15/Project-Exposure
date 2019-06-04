using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldInputField : Raycastable
{
	[SerializeField] private InputField m_inputField;
	[SerializeField] private VirtualKeyboard m_virtualKeyboard;

	public override void Click(Ray ray)
	{
		base.Click(ray);
		
		m_virtualKeyboard.ShowKeyboard(m_inputField);
	}
}
