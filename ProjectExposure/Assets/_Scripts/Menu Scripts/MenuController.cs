using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField] private Image[] m_imagesToFade;

	private bool m_inputReceived = false;
	
	// Use this for initialization
	void Start () 
	{
		//CheckToPlayScreenSaverVideo();
		VideoManager.instance.PlayVideo("menuComic");
		StartCoroutine("StartCountdown");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{
			m_inputReceived = true;
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

	public void CheckToPlayScreenSaverVideo()
	{
		if (!m_inputReceived)
		{
			m_inputReceived = false;
			DOVirtual.DelayedCall(10, delegate {
				VideoManager.instance.PlayVideo("menuComic");
			
			});
		}
	}
	
	public IEnumerator StartCountdown()
	{
		
		yield return new WaitForSeconds(13f);
		if (!m_inputReceived)
		{
			VideoManager.instance.PlayVideo("menuComic");
			m_inputReceived = false;
		}
		
	}
}
