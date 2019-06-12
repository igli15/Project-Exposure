using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Hittable {

    public Action onHit;

    public override void Hit(AbstractGun gun, float damage)
    {
        if (null != onHit) onHit();
    }

    public override void SetColor(Color newColor)
    {
        base.SetColor(newColor);
    }

}
