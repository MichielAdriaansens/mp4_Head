using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LoadScreen : MonoBehaviour
{
    public VideoClip  loadingScreenClip;
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        videoPlayer.clip = loadingScreenClip;
    }

}
