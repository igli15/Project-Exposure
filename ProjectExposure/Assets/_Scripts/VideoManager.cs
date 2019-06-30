using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;


public class VideoManager : MonoBehaviour
{
    public static Action<Video> OnVideoPlay;
    
    [SerializeField] private RawImage m_rawImage;
    [SerializeField] private Video[] m_videos;
    
    [SerializeField] private bool m_closeOnStop = true;
    [SerializeField] [Range(0,2)] private float m_fadeInTime = 0.5f;
    [SerializeField] [Range(0,2)] private float m_fadeOutTime = 0.5f;

    private VideoPlayer m_videoPlayer;

    private static VideoManager m_instance;

    public static VideoManager instance
    {
        get { return m_instance; }
    }

    private void Awake()
    {
        m_videoPlayer = GetComponent<VideoPlayer>();
        
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(m_instance.gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        
        PlayVideo("dissolve");
        
    }

    private void StopVideoPlayer()
    { 
        m_rawImage.DOFade(0, m_fadeOutTime);

        m_videoPlayer.Stop();
    }
    
    public void PlayVideo(string clipName)
    {
        Video v = Array.Find(m_videos, video => video.clipName == clipName);
       
        if (v == null)
        {
            Debug.LogWarning("hey you made a typo, check video clip with name:  " + clipName);
            return ;
        }

       if(OnVideoPlay != null) OnVideoPlay(v);

        m_rawImage.DOFade(1, m_fadeInTime);
        m_videoPlayer.clip = v.videoClip;
        m_videoPlayer.Play();
        
        double clipLength = v.videoClip.length;
        
        if(m_closeOnStop) DOVirtual.DelayedCall((float)clipLength + 0.1f, delegate { StopVideoPlayer(); });

    }
    
}
