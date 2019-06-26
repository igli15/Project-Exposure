using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AoeSphere : MonoBehaviour
{

	[SerializeField] 
	private float m_scaleTime = 0.5f;

	public void Activate(float aoeSize)
	{
		Sequence s = DOTween.Sequence();
		s.Append(transform.DOScale(new Vector3(aoeSize, aoeSize, aoeSize), m_scaleTime));
		s.Append(transform.DOScale(new Vector3(0, 0, 0), m_scaleTime));
	}
}
