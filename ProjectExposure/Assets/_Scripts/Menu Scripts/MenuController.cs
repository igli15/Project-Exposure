using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour 
{
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
}
