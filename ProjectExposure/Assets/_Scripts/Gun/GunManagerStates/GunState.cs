using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(TouchManager))]
public abstract class GunState : AbstractState<GunManager>
{
    public abstract void Shoot();
    public abstract void SetGunColor(Color c);

    protected bool m_canShoot = true;

    protected TouchManager m_touchManager;
    private void Start()
    {
        m_touchManager = GetComponent<TouchManager>();
        Input.multiTouchEnabled = true;
    }

    protected virtual void FixedUpdate()
    {
        Debug.Log(m_touchManager.GetIndexTheFirstTouchOnShootingArea());
        if (Input.GetMouseButtonDown(0) && m_canShoot)
        {
            if (isInsideShootingArea())
            {
                m_canShoot = false;
                DOVirtual.DelayedCall(0.1f, delegate { m_canShoot = true; });
                Shoot();
            }
        }
    }

    private bool isInsideShootingArea()
    {
        float minX = target.shootingArea.anchorMin.x;
        float maxX = target.shootingArea.anchorMax.x;
        
        float minY = target.shootingArea.anchorMin.y;
        float maxY = target.shootingArea.anchorMax.y;

        Vector2 mouseViewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        if (mouseViewPos.x > minX && mouseViewPos.x < maxX && mouseViewPos.y > minY && mouseViewPos.y < maxY)
        {
            return true;
        }

        return false;
    }
}
