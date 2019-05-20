using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PathEvent : UnityEvent<MovementPoint> { }

public class MovementPoint : MonoBehaviour {
    [SerializeField]
    private MovementPoint m_nextPoint;
    [SerializeField]
    private Path m_path;
    public PathEvent onPointActivated;

    public void SetNextPoint(MovementPoint nextPoint)
    {
        m_nextPoint = nextPoint;
    }

    public void ActivatePoint()
    {
        onPointActivated.Invoke(this);
    }

    public void SetPath(Path path)
    {
        m_path = path;
    }

    public Path GetPath()
    {
        return m_path;
    }

    public MovementPoint GetNextPoint()
    {
        if (m_nextPoint == null)
        {
            m_path.ShowPathChoiceButton();
        }
        return m_nextPoint;
    }

    private void OnDrawGizmos()
    {
        return;
        Gizmos.color = Color.green;
        m_path.color.a = 1;
        if (m_path != null) Gizmos.color = m_path.color;

        Gizmos.DrawWireSphere(transform.position, 0.2f);
        if(m_nextPoint!=null)
            Gizmos.DrawLine(transform.position, m_nextPoint.transform.position);
    }
    private void OnDrawGizmosSelected()
    {
        return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);
        if (m_nextPoint != null)
            Gizmos.DrawLine(transform.position, m_nextPoint.transform.position);
    }
}
