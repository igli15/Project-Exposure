using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JumpAnim : MonoBehaviour 
{
	
	[SerializeField] private Transform m_endPos;

	[SerializeField] private GameObject m_bubbles;
    public VideoManager vidMan;
	
	public void Jump()
	{
		vidMan.PlayVideo("krakenSwims");

        transform.DOScale(Vector3.zero, 2);
		transform.DORotate(new Vector3(transform.rotation.eulerAngles.x + 60, transform.rotation.eulerAngles.y , transform.rotation.eulerAngles.z), 1);
		transform.DOJump(m_endPos.position, 60, 1, 3);
		m_bubbles.SetActive(true);
	}
}
