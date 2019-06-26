using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathButton : MonoBehaviour {

	[SerializeField]
	private Path m_optionalPath;
	public void TakeOptPath()
	{
		RailMovement.instance.SetPoint(m_optionalPath.GetFirstPoint());
		RailMovement.instance.StartMovement();
	}
}
