using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveCameraButton : WorldButton
{
	[SerializeField] private Transform m_finalCameraPos;

	public override void Click(Ray ray)
	{
		base.Click(ray);

		Camera.main.transform.DOMove(m_finalCameraPos.position,4);
		Camera.main.transform.DORotate(m_finalCameraPos.rotation.eulerAngles, 3);
	}
}
