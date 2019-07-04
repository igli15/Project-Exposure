using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePortal : MonoBehaviour {

    [SerializeField] private float m_speed = 50;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * m_speed);
	}
}
