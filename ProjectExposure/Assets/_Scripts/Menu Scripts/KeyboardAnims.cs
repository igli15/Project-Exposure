using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class KeyboardAnims : MonoBehaviour
{
	public void MoveDown()
	{
		transform.DOMoveY(transform.position.y-5, 0.5f);
	}

	public void MoveUp()
	{
		transform.DOMoveY(transform.position.y + 5, 0.5f);
	}
}
