using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{

	[SerializeField] private UnityEvent OnClick;

	public virtual void Click()
	{
		OnClick.Invoke();
	}
}
