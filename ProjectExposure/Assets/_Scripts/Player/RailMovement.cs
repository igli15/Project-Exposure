using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RailMovement : MonoBehaviour
{
    public Slider slider;
    public float speed = 0.5f;
    public Transform firstMovementPoint;

    private Vector3 m_targetPoint;
    private Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();

        m_targetPoint = firstMovementPoint.transform.position;
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
        Vector3 direction = m_targetPoint-transform.position;
        m_rb.velocity = direction.normalized*speed;
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
            Debug.Log("NewPoint Reached");
            if (other.GetComponent<MovementPoint>().isEndPoint)
            {
                StopMovement();
                return;
            }
            m_targetPoint = other.GetComponent<MovementPoint>().GetNextPosition();
            Tweener tweener = transform.DOLookAt(m_targetPoint, 2);
            StartMovement();
        }
            
    }
}
