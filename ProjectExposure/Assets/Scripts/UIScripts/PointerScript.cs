using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{
	[SerializeField] 
	private Gun targetGun;
	
	[SerializeField]
	private RectTransform m_barRectTransform;
	
	private RectTransform m_transform;

	private float m_parentHalfWidth;
	
	// Use this for initialization
	void Awake ()
	{
		m_transform = GetComponent<RectTransform>();
		m_parentHalfWidth = m_barRectTransform.rect.width/2;

		targetGun.OnChargeChanged += SetPositionBasedOnHue;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	void SetPositionBasedOnHue(Gun gun)
	{
		float pos = ((gun.Hue()/300) * 2 -1) * m_parentHalfWidth ; //flip it also
		
		m_transform.anchoredPosition = new Vector2(pos,m_transform.anchoredPosition.y);
	}
}
