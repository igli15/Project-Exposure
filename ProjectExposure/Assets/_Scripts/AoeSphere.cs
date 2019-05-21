using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AoeSphere : MonoBehaviour
{

	[SerializeField] 
	private float m_scaleTime = 0.5f;

	[SerializeField] 
	private float m_aoeSize = 3;

	private void OnEnable()
	{
		Sequence s = DOTween.Sequence();
		s.Append(transform.DOScale(new Vector3(m_aoeSize, m_aoeSize, m_aoeSize), m_scaleTime));
		s.Append(transform.DOScale(new Vector3(0, 0, 0), m_scaleTime));
	}
}
