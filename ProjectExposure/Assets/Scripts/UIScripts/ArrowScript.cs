using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowScript : MonoBehaviour
{

	enum ArrowType
	{
		LEFT,
		RIGHT
	}
	
	[SerializeField] 
	private Gun m_gun;

	[SerializeField] 
	private ArrowType m_arrowType;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton(0) && IsMouseOver())
		{
			if (m_arrowType == ArrowType.RIGHT)
			{
				m_gun.DecreaseCharge();
			}
			else
			{
				m_gun.IncreaseCharge();
			}
		}
	}
	
	
	bool IsMouseOver()
	{
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = Input.mousePosition;

		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData,results);
		GameObject obj = null;
		for (int i = 0; i < results.Count; i++)
		{
			if (results[i].gameObject.GetInstanceID() == gameObject.GetInstanceID())
			{
				obj = results[i].gameObject;
			}
		}

		if (obj != null && obj.GetInstanceID() == this.gameObject.GetInstanceID())
		{
			return true;
		}
		else return false;
	}
}
