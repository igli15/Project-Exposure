using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPoint : MonoBehaviour {

    public int id = 0;
    public bool isEndPoint = false;

    private MovementPoint m_nextPoint;
	void Start () {

	}

    public void SetNextPoint(MovementPoint nextPoint)
    {
        m_nextPoint = nextPoint;
    }

    public Vector3 GetNextPosition()
    {
        return m_nextPoint.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1);
        if(m_nextPoint!=null)
            Gizmos.DrawLine(transform.position, m_nextPoint.transform.position);
    }
}
