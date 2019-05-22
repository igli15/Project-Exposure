using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarCrystalSmash : MonoBehaviour
{

    [SerializeField]
    private GameObject m_crystal;

    private void OnTriggerEnter(Collider other)
    {
        if (m_crystal != null)
        {
            Destroy(m_crystal.transform.gameObject);
            Debug.Log("i collided");
        }
    }
}
