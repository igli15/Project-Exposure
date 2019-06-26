using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Path : MonoBehaviour {
    [Header("Next Paths")]
    [SerializeField]
    private List<Path> m_paths;
    [SerializeField]
    public Button button;

    [Header("Settings")]
    [SerializeField]
    float radiusOfPoint=0.5f;
    [SerializeField]
    public Color color;
    [SerializeField]
    private int m_pointCount = 0;
    [SerializeField]
    private MovementPoint m_firstPoint;

    private int m_currentCount = 0;
    [SerializeField]
    private MovementPoint m_lastPoint = null;

    public List<MovementPoint> points;


    public void Start()
    {
        if(button!=null)
            button.gameObject.SetActive(false);
    }

    public static List<MovementPoint> GetAllPoints(MovementPoint startPoint)
    {
        List<MovementPoint> buffer_points=new List<MovementPoint>();
        while (startPoint.GetNextPoint() != null)
        {
            buffer_points.Add(startPoint);
            startPoint = startPoint.GetNextPoint();
        }

        return buffer_points;
    }

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);
    }

    public void SetPointCount(int pointCount)
    {
        m_pointCount = pointCount;
    }

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
            collider.radius = radiusOfPoint;

            MovementPoint mp=newPoint.AddComponent<MovementPoint>();
            mp.SetPath(this);
            if (i > 0) m_lastPoint.SetNextPoint(mp);
            if (i == 0) { m_firstPoint = mp;}

            m_lastPoint = mp;
        }

        EditorTools.SafeDestroyGameObject<Transform>(empty.transform);
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
        collider.radius = radiusOfPoint;

        m_currentCount++;
        m_pointCount = m_currentCount;
        MovementPoint mp = newPoint.AddComponent<MovementPoint>();
        mp.SetPath(this);
        m_lastPoint.SetNextPoint(mp);

        m_lastPoint = mp;

        EditorTools.SafeDestroyGameObject<Transform>(empty.transform);
    }

    public void DestroyPoints()
    {
        while(transform.childCount>0)
        {
            EditorTools.SafeDestroyGameObject<Transform>(transform.GetChild(0));
        }
    }

    public MovementPoint GetLastPoint()
    {
        return m_lastPoint;
    }

    public MovementPoint GetFirstPoint()
    {
        return m_firstPoint;
    }

    public void ShowPathChoiceButton()
    {
        foreach (Path path in m_paths)
        {
            if (path.button != null)
            {
                path.button.gameObject.SetActive(true);
                path.button.onClick.AddListener(() =>
                {
                    RailMovement.instance.SetPoint(path.GetFirstPoint());
                    RailMovement.instance.StartMovement();
                    foreach (Path cpath in m_paths)
                    {
                        cpath.button.gameObject.SetActive(false);
                    }
                });
            }
        }
    }
}
