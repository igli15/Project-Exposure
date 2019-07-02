using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayKrakenVideo2nd : MonoBehaviour {
    public VideoManager vidMan;
    private bool m_alreadyPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") &&m_alreadyPlayed == false)
        {
            vidMan.videoPlayer.waitForFirstFrame = true;
            vidMan.PlayVideo("krakenCave");
            m_alreadyPlayed = true;
        }
    }
}
