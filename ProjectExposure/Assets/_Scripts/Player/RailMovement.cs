using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMovement : MonoBehaviour
{

    public float speed = 0.5f;

    private Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        StartMovement();
        Debug.Log("start");
    }

    void Update()
    {

    }

    public void StopMovement()
    {
        Debug.Log("stop");
        m_rb.velocity = Vector3.zero;
    }

    public void StartMovement()
    {
        m_rb.velocity = new Vector3(0, 0, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colliaion");
        if (other.CompareTag("EnemyZone"))
        {
            Debug.Log("inside zone");
            other.GetComponent<EnemySpawner>().SpawnEnemies();
            other.GetComponent<EnemySpawner>().railMovement = this;
            StopMovement();
        }
            
    }
}
