using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GateStopper : MonoBehaviour {
    [SerializeField]
    private CompanionButton m_compButton;
    [SerializeField]
    private string m_showButton;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_compButton.ShowButton(m_showButton);
            CurveWallker.instance.StopMovement();
        }
    }
}
