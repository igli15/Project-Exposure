using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RailMovement : MonoBehaviour
{
    public Slider slider;
    public float speed = 0.5f;
    public float rotationTime = 1;
    public Path initialPath;

    private MovementPoint m_targetPoint;
    private Rigidbody m_rb;
    public static RailMovement instance;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        instance = this;

        m_targetPoint = initialPath.GetFirstPoint();
        StartMovement();
    }

    void Update()
    {

    }

    public void StopMovement()
    {
        m_rb.velocity = Vector3.zero;
    }

    public void StartMovement()
    {
        Debug.Log("START");
        Vector3 direction = m_targetPoint.transform.position-transform.position;
        m_rb.velocity = direction.normalized*speed;
        Tweener tweener = transform.DOLookAt(m_targetPoint.transform.position, rotationTime);
    }

    public void SetPoint(MovementPoint point)
    {
        m_targetPoint = point;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovementPoint"))
        {
            if (other.GetComponent<MovementPoint>().GetNextPoint()==null)
            {
                //Stop movement on the end of path
                StopMovement();
                return;
            }
            MovementPoint bufferPoint = m_targetPoint;

            //Move forward
            m_targetPoint = other.GetComponent<MovementPoint>().GetNextPoint();
            StartMovement();

            //Activate all events binded to Point
            bufferPoint.ActivatePoint();
        }
    }
}
