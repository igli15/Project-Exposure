using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RailMovement : MonoBehaviour
{
    public Slider slider;
    public float speed = 0.5f;

    private Vector3 m_direction;
    private Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
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
        m_direction = dir;
    }

    public void StopMovement()
    {
        m_rb.velocity = Vector3.zero;
    }

    public void StartMovement()
    {
        m_rb.velocity = new Vector3(0, 0, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyZone"))
        {
            other.GetComponent<EnemySpawner>().SpawnEnemies();
            other.GetComponent<EnemySpawner>().railMovement = this;
            StopMovement();
        }
            
    }
}
