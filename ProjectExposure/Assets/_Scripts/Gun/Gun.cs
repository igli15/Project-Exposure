using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
	public enum GunSide
	{
		LEFT,
		RIGHT
	}

	[SerializeField] private GunSide m_gunSide;
	[SerializeField] private Transform m_shootTransform;


	[SerializeField] private GunEffectGroup[] gunEffectGroups;

	public int GetEffectGroupCount()
	{
		return gunEffectGroups.Length;
	}
	
	public GunEffectGroup GetEffectGroupAt(int index)
	{
		return gunEffectGroups[index];
	}

	public GunSide gunSide
	{
		get { return m_gunSide; }
	}

	public Transform shootTransform
	{
		get { return m_shootTransform; }
	}
}
