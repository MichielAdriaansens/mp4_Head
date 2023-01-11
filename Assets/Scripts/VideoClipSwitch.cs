using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoClipSwitch : MonoBehaviour
{
    public MicrophoneInput micInput;
    private VideoPlayer vidPlayer;
    public VideoClip clip1;
    public VideoClip clip2;
    
    // Start is called before the first frame update
    void Awake()
    {
        vidPlayer = gameObject.GetComponent<VideoPlayer>();
        vidPlayer.clip = clip1;
    }

    private void Update()
    {
        if (micInput.micTrigger)
        {
            vidPlayer.clip = clip2;
        }
        else
        {
            vidPlayer.clip = clip1;
        }
    }
}
