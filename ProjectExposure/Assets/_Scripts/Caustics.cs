using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caustics : MonoBehaviour
{
	public float fps = 30.0f;
	public Texture2D[] frames;

	private int m_frameIndex;
	private Projector m_projector;
	

	// Use this for initialization
	void Start () 
	{
		m_projector = GetComponent<Projector> ();
		NextFrame();
		InvokeRepeating ("NextFrame", 1 / fps, 1 / fps);
	}
	
	void NextFrame(){
		m_projector.material.SetTexture ("_ShadowTex", frames [m_frameIndex]);
		m_frameIndex = (m_frameIndex + 1) % frames.Length;
	}

}
