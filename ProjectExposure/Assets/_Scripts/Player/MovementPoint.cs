using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPoint : MonoBehaviour {

    [SerializeField]
    private MovementPoint m_nextPoint;

    public void SetNextPoint(MovementPoint nextPoint)
    {
        m_nextPoint = nextPoint;
    }

    public MovementPoint GetNextPoint()
    {
        return m_nextPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1);
        if(m_nextPoint!=null)
            Gizmos.DrawLine(transform.position, m_nextPoint.transform.position);
    }
}
