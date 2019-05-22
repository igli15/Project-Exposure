using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacles : Hittable {

    [SerializeField]
    private GameObject m_inkSpray;
    public override void Hit(GunManager gunManager, float damage, Color gunColor)
    {
        Debug.Log("suck my tit");
        m_inkSpray.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
