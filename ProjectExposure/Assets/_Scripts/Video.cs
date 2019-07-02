using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
public class Video 
{
	public string clipName;
	
	public VideoClip videoClip;

	[HideInInspector] public VideoPlayer videoPlayer;
	[HideInInspector] public RenderTexture renderTexture;
}
