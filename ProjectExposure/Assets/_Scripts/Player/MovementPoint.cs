using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPoint : MonoBehaviour {

    [SerializeField]
    private MovementPoint m_nextPoint;
    [SerializeField]
    private Path m_path;
    public void SetNextPoint(MovementPoint nextPoint)
    {
        m_nextPoint = nextPoint;
    }

    public void SetPath(Path path)
    {
        m_path = path;
    }

    public MovementPoint GetNextPoint()
    {
        if (m_nextPoint == null)
        {
            m_path.ShowHudOptions();
        }
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
