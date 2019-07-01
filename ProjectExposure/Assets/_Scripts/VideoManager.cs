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
    public static Action<Video> OnVideoStop;
    
    [SerializeField] private RawImage m_rawImage;
    [SerializeField] private Video[] m_videos;
    
    
    [SerializeField] private bool m_closeOnStop = true;
    [SerializeField] [Range(0,1)] private float m_timeScale = 0;
    [SerializeField] [Range(0,2)] private float m_fadeInTime = 0.5f;
    [SerializeField] [Range(0,2)] private float m_fadeOutTime = 0.1f;

    private VideoPlayer m_videoPlayer;

    private static VideoManager m_instance;

    public static VideoManager instance
    {
        get { return m_instance; }
    }

    public VideoPlayer videoPlayer
    {
        get { return m_videoPlayer; }
    }

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        DontDestroyOnLoad(gameObject);
        
        m_videoPlayer = GetComponent<VideoPlayer>();

    }

    private void StopVideoPlayer(Video v)
    {
        if(OnVideoStop != null) OnVideoStop(v);
        Time.timeScale = 1;
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

        Tween f=  m_rawImage.DOFade(1, m_fadeInTime);
        f.onComplete += delegate { Time.timeScale = m_timeScale; };
        
        m_videoPlayer.clip = v.videoClip;
        m_videoPlayer.Play();
        
        double clipLength = v.videoClip.length;
        
        if(m_closeOnStop) DOVirtual.DelayedCall((float)clipLength + 0.1f, delegate { StopVideoPlayer(v); });

    }
    
}
