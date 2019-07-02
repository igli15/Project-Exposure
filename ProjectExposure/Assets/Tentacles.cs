using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacles : Hittable {

    [SerializeField]
    private GameObject m_inkSpray;
    public override void Hit(AbstractGun gun, float damage)
    {
        m_inkSpray.gameObject.SetActive(true);
    }
}
