using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField] private Image[] m_imagesToFade;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray.origin,ray.direction, out hit, 2000))
			{
				Raycastable raycastable = hit.transform.GetComponent<Raycastable>();
				if(raycastable != null) raycastable.Click(ray);
			}
		}
	}

	public void FadeElements()
	{
		foreach (Image i in m_imagesToFade)
		{
			i.DOFade(0,0.3f).onComplete += delegate { i.gameObject.SetActive(false); };
		}
	}
}
