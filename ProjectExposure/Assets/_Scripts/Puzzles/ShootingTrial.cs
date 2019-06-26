using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShootingTrial : MonoBehaviour {

	[SerializeField]
	private float m_duration;
	[SerializeField]
	private float m_magnitude;
	// Use this for initialization
	[SerializeField]
	private List<GameObject> m_crystals;
	[SerializeField]
	private GameObject m_doorLeft;
	[SerializeField]
	private GameObject m_doorRight;
	private bool m_destroyed = false;
	public CameraShake cameraShake;
	void Start ()
	{
		foreach (var c in m_crystals)
		{
			c.GetComponent<Crystal>().OnExplode += delegate(Crystal crystal)
			{
				m_crystals.Remove(c); 
			};
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_crystals.Count<=0 & m_destroyed == false)
		{
			cameraShake.Shake(m_duration, m_magnitude);
			Invoke("ContinueMoving", m_duration);
			m_crystals.Clear();
			m_doorLeft.transform.DOLocalMoveX(-0.9f, m_duration);
			m_doorRight.transform.DOMoveX(3.9f, m_duration);
			m_destroyed = true;
		}

	}

	void ContinueMoving()
	{
		RailMovement.instance.StartMovement();
	}
}
