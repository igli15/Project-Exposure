using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHandle : MonoBehaviour
{
	public Action<SliderHandle> OnSliderDragged;

	public void OnMouseDrag()
	{		
		Debug.Log("Dragged");
		if(OnSliderDragged != null) OnSliderDragged(this);
	}
	
}
