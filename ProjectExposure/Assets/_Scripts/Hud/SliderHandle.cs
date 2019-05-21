using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHandle : MonoBehaviour
{
	public Action<SliderHandle> OnSliderDragged;

	private void OnMouseDrag()
	{		
		if(OnSliderDragged != null) OnSliderDragged(this);
	}

	
}
