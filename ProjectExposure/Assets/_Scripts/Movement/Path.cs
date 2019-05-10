using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    [SerializeField]
    private int m_pointCount = 0;
    private MovementPoint[] m_points;
    

    void Start () {
        //m_points = new MovementPoint[m_pointCount];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GeneratePoints()
    {
        if (m_pointCount < 0) m_pointCount = 0;
        if (m_pointCount > 40) m_pointCount = 40;
        DestroyPoints();
        GameObject empty = new GameObject();
        m_points = new MovementPoint[m_pointCount];

        MovementPoint bufferPoint = null;

        for (int i = 0; i < m_pointCount; i++)
        {
            GameObject newPoint = GameObject.Instantiate(empty, transform.position + new Vector3(0, 0, 2 * i), transform.rotation,transform);
            newPoint.name = "point_" + i;

            MovementPoint mp=newPoint.AddComponent<MovementPoint>();
            
            if (i > 0)
            {
                bufferPoint.SetNextPoint(mp);
            }
            bufferPoint = mp;
            if (i == m_pointCount - 1) mp.isEndPoint = true;
        }
        PathEditor.SafeDestroyGameObject<Transform>(empty.transform);
    }
    public void DestroyPoints()
    {
        
        while(transform.childCount>0)
        {
            PathEditor.SafeDestroyGameObject<Transform>(transform.GetChild(0));
        }
    }


}
