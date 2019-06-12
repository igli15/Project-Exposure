using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateStopper : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CurveWallker.instance.StopMovement();
        }
    }
}
