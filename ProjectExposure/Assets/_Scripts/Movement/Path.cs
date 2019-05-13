using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {
    [Header("Next Paths")]
    [SerializeField]
    private List<Path> m_paths;

    [Header("Settings")]
    [SerializeField]
    public Color color;
    [SerializeField]
    private int m_pointCount = 0;
    [SerializeField]
    private MovementPoint m_firstPoint;

    private int m_currentCount = 0;
    [SerializeField]
    private MovementPoint m_lastPoint = null;


    public void GeneratePoints()
    {
        if (m_pointCount < 0) m_pointCount = 0;
        if (m_pointCount > 40) m_pointCount = 40;
        m_currentCount = m_pointCount;

        DestroyPoints();

        GameObject empty = new GameObject();
        for (int i = 0; i < m_pointCount; i++)
        {
            GameObject newPoint = GameObject.Instantiate(empty, transform.position + new Vector3(0, 0, 8 * i), transform.rotation,transform);
            newPoint.name = "point_" + i;
            newPoint.tag = "MovementPoint";
            SphereCollider collider = newPoint.AddComponent<SphereCollider>();
            collider.radius = 1;

            MovementPoint mp=newPoint.AddComponent<MovementPoint>();
            mp.SetPath(this);
            if (i > 0) m_lastPoint.SetNextPoint(mp);
            if (i == 0) { m_firstPoint = mp; Debug.Log("first point " + m_firstPoint);  }

            m_lastPoint = mp;
        }

        PathEditor.SafeDestroyGameObject<Transform>(empty.transform);
    }

    public void AddPoint()
    {
        if (transform.childCount == 0)
        {
            Debug.Log("First generate path!");
            return;
        }
        GameObject empty = new GameObject();

        GameObject newPoint = GameObject.Instantiate(empty, m_lastPoint.transform.position+new Vector3(0,0,2), transform.rotation, transform);
        newPoint.name = "point_" + m_currentCount;
        newPoint.tag = "MovementPoint";
        SphereCollider collider = newPoint.AddComponent<SphereCollider>();
        collider.radius = 1;

        m_currentCount++;
        m_pointCount = m_currentCount;
        MovementPoint mp = newPoint.AddComponent<MovementPoint>();
        mp.SetPath(this);
        m_lastPoint.SetNextPoint(mp);

        m_lastPoint = mp;

        PathEditor.SafeDestroyGameObject<Transform>(empty.transform);
    }

    public void DestroyPoints()
    {
        
        while(transform.childCount>0)
        {
            PathEditor.SafeDestroyGameObject<Transform>(transform.GetChild(0));
        }
    }

    public MovementPoint GetFirstPoint()
    {
        return m_firstPoint;
    }

    public void ShowHudOptions()
    {
        PathChoiceManager.instance.GetComponent<PathChoiceManager>().GenerateButtons(m_paths);
    }
}
