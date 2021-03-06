﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour {
    [SerializeField]
    BossViewScript m_boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("OBJ: " + other.name);
            m_boss.ActivateBossFight();
        }
    }
}
