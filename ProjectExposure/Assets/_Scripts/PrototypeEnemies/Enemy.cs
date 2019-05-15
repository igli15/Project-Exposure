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
     
    }

    public override void Hit(GunManager gunManager)
    {
        base.Hit(gunManager);

        if (gunManager.currentMode == GunManager.GunMode.COLOR)
        {
            SetColor(gunManager.colorGun.GetColor());
        }
        else if (gunManager.currentMode == GunManager.GunMode.MAGNET && gunManager.magnetGun.pulledTransform == null)
        {
            transform.DOMove(gunManager.magnetGun.pullTargetLocation.position, 2.0f);
            gunManager.magnetGun.pulledTransform = transform;
        }
        else
        {
            m_health.InflictDamage(gunManager.damage);
        }
    }
}
