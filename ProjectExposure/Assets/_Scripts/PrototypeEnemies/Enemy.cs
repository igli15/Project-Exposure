using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : Hittable
{
    //public Color color;

    private Health m_health;

    public void Start()
    {
        SetColor(color);
        m_health = GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) m_health.InflictDamage(2000000000);
    }

}
