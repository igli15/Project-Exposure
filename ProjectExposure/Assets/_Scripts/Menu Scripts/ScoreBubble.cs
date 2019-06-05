using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreBubble : MonoBehaviour,IPointerDownHandler
{
	
	private TextMeshProUGUI m_bubbleText;
	private RectTransform m_rectTransform;
	private Rigidbody2D m_rigidbody2D;

	// Use this for initialization
	void Start ()
	{
		m_bubbleText = GetComponentInChildren<TextMeshProUGUI>();
		m_rigidbody2D = GetComponent<Rigidbody2D>();
		m_rectTransform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_rectTransform.anchoredPosition.y >= 80) Spawn();
	}

	public void Spawn(string s)
	{
		if (m_rigidbody2D != null)
		{
			//m_rigidbody2D.gravityScale = 0;
			m_rigidbody2D.velocity = Vector2.zero;
			m_rectTransform.anchoredPosition = new Vector3(Random.Range(-20, 26), 0.5f, 0);
		}

		DOVirtual.DelayedCall(Random.Range(1f,5.0f), delegate
		{
			m_bubbleText.text = s;
			m_rigidbody2D.velocity = Vector2.up * 2f;
		});
	}

	private void Spawn()
	{
		if (m_rigidbody2D != null)
		{
			//m_rigidbody2D.gravityScale = 0;
			m_rigidbody2D.velocity = Vector2.zero;
			m_rectTransform.anchoredPosition = new Vector3(Random.Range(-20, 26), 0.5f, 0);
		}
		
		DOVirtual.DelayedCall(Random.Range(1,10f), delegate
			{
				m_rigidbody2D.velocity = Vector2.up * 2f;
			});
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Spawn();
	}
}
