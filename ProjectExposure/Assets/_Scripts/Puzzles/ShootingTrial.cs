using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrial : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private List<GameObject> m_crystals;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(m_crystals.Count<=0)
		{
			RailMovement.instance.StartMovement();
		}
	}
}
