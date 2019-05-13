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
        Debug.Log("FIRST POINT" + m_targetPoint);
        StartMovement();

        //Health part
        //TODO: move to separate script
        Health health = GetComponent<Health>();
        slider.maxValue = health.MaxHealth;
        health.OnHealthDecreased += OnHealthChanged;
        health.OnHealthIncreased += OnHealthChanged;
        OnHealthChanged(health);
    }

    void OnHealthChanged(Health health)
    {
        slider.value = health.HP;
    }

    void Update()
    {

    }

    public void SetDirecton(Vector3 dir)
    {
        //m_direction = dir.normalized;
        //tweener.OnComplete(() => { StartMovement(); });
    }

    public void StopMovement()
    {
        m_rb.velocity = Vector3.zero;
    }

    public void StartMovement()
    {
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
        if (other.CompareTag("EnemyZone"))
        {
            other.GetComponent<EnemySpawner>().SpawnEnemies();
            other.GetComponent<EnemySpawner>().railMovement = this;
            StopMovement();
        }
        if (other.CompareTag("MovementPoint"))
        {
            
            if (other.GetComponent<MovementPoint>().GetNextPoint()==null)
            {
                StopMovement();
                return;
            }
            m_targetPoint = other.GetComponent<MovementPoint>().GetNextPoint();
            //Tweener tweener = transform.DOLookAt(m_targetPoint.transform.position, rotationTime);
            StartMovement();
        }
            
    }
}
