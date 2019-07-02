using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoActivator : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        VideoManager.instance.PlayVideo("krakenFight");
    }
}
