using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrial : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private List<GameObject> m_crystals;
	private bool m_destroyed = false;
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
			RailMovement.instance.StartMovement();
			m_crystals.Clear();
			m_destroyed = true;
		}
	}
}
