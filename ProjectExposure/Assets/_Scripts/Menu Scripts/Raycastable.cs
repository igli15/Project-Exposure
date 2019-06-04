using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Raycastable : MonoBehaviour
{

	[SerializeField] private UnityEvent OnMouseDown;

	public virtual void Click(Ray ray)
	{
		OnMouseDown.Invoke();
	}
}
