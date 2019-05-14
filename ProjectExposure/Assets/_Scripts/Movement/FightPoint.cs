using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FightPoint : MonoBehaviour
{
    [SerializeField]
    private float m_timeOfRotation;
    [SerializeField]
    private int m_enemypathsCount=0;

    private bool m_isActivated=false;

    public void ActivateFightPoint()
    {
        m_isActivated = true;

        RailMovement.instance.StopMovement();
        RailMovement.instance.transform.DORotateQuaternion(transform.rotation, m_timeOfRotation);
    }

    public void GenerateEnemyPaths()
    {
        DestroyEnemyPaths();
        for (int i = 0; i < m_enemypathsCount; i++)
        {
            GameObject empty = new GameObject();
            GameObject newEnemyPath = GameObject.Instantiate(empty, transform.position + new Vector3(5*i, 0, 5), transform.rotation, transform);
            newEnemyPath.name = "EnemyPath";

            Path path=newEnemyPath.AddComponent<Path>();
            path.SetPointCount(3);
            path.GeneratePoints();

            EditorTools.SafeDestroyGameObject<Transform>(empty.transform);
        }
    }

    public void DestroyEnemyPaths()
    {
        while (transform.childCount > 0)
        {
            EditorTools.SafeDestroyGameObject<Transform>(transform.GetChild(0));
        }
    }

    private void Update()
    {
        if (!m_isActivated) return;

        if (Input.GetKeyDown(KeyCode.O))
        {
            RailMovement.instance.StartMovement();
            m_isActivated = false;
        }

    }

    private void OnDrawGizmos()
    {
        //Drawing the frustrum of the camera with points position and points rotation
        Gizmos.color = Color.red;
        Matrix4x4 temp = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Camera camera = Camera.main;
        Gizmos.DrawFrustum(Vector3.zero, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane, camera.aspect);
        Gizmos.matrix = temp;
    }
}
