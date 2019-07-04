using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
	public enum TableType
	{
		DAILY,
		YEARLY
	}
	
	[SerializeField] private VirtualKeyboard m_virtualKeyboard;

	private TextMeshProUGUI m_textMeshPro;

	[SerializeField] private TableType m_type = TableType.DAILY;

	[SerializeField] private Image[] m_highlighters;

	[SerializeField] private Image[] m_crowns;

	// Use this for initialization
	void Start ()
	{
		m_textMeshPro = GetComponent<TextMeshProUGUI>();

		m_virtualKeyboard.OnSave.AddListener(SetText);
	}

	public void SetText()
	{
		HighScoreManager.instance.LoadHighScores();
		IOrderedEnumerable<KeyValuePair<string, int>> scores = null;
		
		if(m_type == TableType.DAILY) scores = HighScoreManager.instance.orderedScores;
		else if(m_type == TableType.YEARLY) scores = HighScoreManager.instance.orderedScoresYearly;

		string finalText = "";

		var scoreList = scores.ToList();
		int index = -1;
		for (int i = 0; i < scoreList.Count; i++)
		{
			if (m_virtualKeyboard.inputField.text == scoreList[i].Key)
			{
				index = i;
			}
		}
		if(index != -1) m_highlighters[index].gameObject.SetActive(true);
		
		int count = 0;
		
		if (scores.Count() < 10) count = scores.Count();
		else count = 10;
		
		for (int i = 0; i <count; i++)
		{
			finalText += scores.ElementAt(i).Key + ": " + scores.ElementAt(i).Value + "\n";
		}

		if (scoreList.Count > 0)
		{
			m_crowns[0].gameObject.SetActive(true);
		}
		
		if (scoreList.Count > 1)
		{
			m_crowns[1].gameObject.SetActive(true);
		}
		
		if (scoreList.Count > 2)
		{
			m_crowns[2].gameObject.SetActive(true);
		}

		m_textMeshPro.text = finalText;
	}
}
