using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldInputField : Raycastable
{
	[SerializeField] private TMP_InputField m_inputField;
	[SerializeField] private VirtualKeyboard m_virtualKeyboard;

	private void Start()
	{
		m_virtualKeyboard.ShowKeyboard(m_inputField);
	}
}
