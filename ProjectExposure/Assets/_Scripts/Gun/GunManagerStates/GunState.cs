using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunState : AbstractState<GunManager>
{

    public abstract void Shoot();

    protected virtual void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
}
