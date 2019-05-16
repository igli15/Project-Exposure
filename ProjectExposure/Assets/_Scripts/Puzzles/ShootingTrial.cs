using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrial : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private List<GameObject> m_crystals;
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
		if(m_crystals.Count<=0)
		{
			RailMovement.instance.StartMovement();
		}
	}
}
