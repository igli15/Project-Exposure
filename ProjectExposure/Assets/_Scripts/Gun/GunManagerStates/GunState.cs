using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class GunState : AbstractState<GunManager>
{
    public abstract void Shoot();
    public abstract void SetGunColor(Color c);

    protected bool m_canShoot = true;

    protected virtual void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && m_canShoot)
        {
            m_canShoot = false;
            DOVirtual.DelayedCall(0.1f, delegate { m_canShoot = true; });
            Shoot();
        }
    }
}
