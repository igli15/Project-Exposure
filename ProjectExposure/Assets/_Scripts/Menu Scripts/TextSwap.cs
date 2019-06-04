using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class TextSwap : MonoBehaviour
{
	public UnityEvent OnSwaped;

	[SerializeField] private Transform m_from;

	[SerializeField] private Transform m_to;
	
	// Use this for initialization
	void Start ()
	{
		
	}

	public void Swap()
	{
		Sequence s = DOTween.Sequence();

		s.Append(m_from.transform.DOScaleX(0, 0.2f));
		
		s.Append(m_to.transform.DOScaleX(1, 0.2f));
		s.onComplete += delegate { OnSwaped.Invoke(); };
		
	}
}
