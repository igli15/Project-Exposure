using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPoint : MonoBehaviour {

    public int id = 0;
    public bool isEndPoint = false;

    public static List<MovementPoint> m_movementPoints=new List<MovementPoint>();

	void Start () {
        Debug.Log("THIS: " +id+" | "+ this);
        Debug.Log("m_movementPoints " + m_movementPoints);

        if (!m_movementPoints.Contains(this))
        {
            Debug.Log("Adding new Movementpoint with id " + id);
            m_movementPoints.Add(this);
        }
	}


    public Vector3 GetNextPosition()
    {
        if (isEndPoint) return transform.position;

        foreach (MovementPoint mp in m_movementPoints)
        {
            if (mp.id == id + 1)
            {
                return mp.transform.position;
            }
        }
        Debug.Assert(true,"Cant find MovementPoint with id "+id);
        return new Vector3(0, 0, 0);
    }
}
