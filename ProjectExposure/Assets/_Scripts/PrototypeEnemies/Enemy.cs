﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Color color;

    public float baseDamage = 10;
    public float extraDamage = 20;
    public float aoeDamage = 5;
    public float range = 30;

    private Health m_health;

    public void Start()
    {
        GetComponent<MeshRenderer>().material.color = color;
        m_health = GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) m_health.InflictDamage(2000000000);
    }

    public void GetDamagedByHue(float damage)
    {
        m_health.InflictDamage(damage);
    }

  
}
