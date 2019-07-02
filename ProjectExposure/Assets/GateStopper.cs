using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GateStopper : MonoBehaviour {
    [SerializeField]
    private CompanionButton m_compButton;
    [SerializeField]
    private string m_showButton;
    public Gate2Behaviour gate2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_compButton.ShowButton(m_showButton);
            if(gate2.alreadySolved == false)
            CurveWallker.instance.StopMovement();
        }
    }
}
