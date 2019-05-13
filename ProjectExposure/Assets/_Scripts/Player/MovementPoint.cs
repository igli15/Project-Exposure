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
        m_path.color.a = 1;
        if (m_path != null) Gizmos.color = m_path.color;

        Gizmos.DrawWireSphere(transform.position, 1);
        if(m_nextPoint!=null)
            Gizmos.DrawLine(transform.position, m_nextPoint.transform.position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 1);
        if (m_nextPoint != null)
            Gizmos.DrawLine(transform.position, m_nextPoint.transform.position);
    }
}
