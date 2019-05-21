using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : Hittable
{
    //public Color color;


    public void Start()
    {
        SetColor(color);

    }

    private void Update()
    {
     
    }

    public override void Hit(GunManager gunManager)
    {
        base.Hit(gunManager);
    }
}
