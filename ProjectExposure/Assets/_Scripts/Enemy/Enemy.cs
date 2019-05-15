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
        
        //OnReleased += delegate(Hittable hittable) { };
    }

    private void Update()
    {
     
    }

    public override void Hit(GunManager gunManager)
    {
        base.Hit(gunManager);
    }
}
