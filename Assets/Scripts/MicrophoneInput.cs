using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MicrophoneInput : MonoBehaviour
{
    public int mics;
    public string selectedDevice;
    public AudioSource _audioSource;
    public AudioMixerGroup micBus;

    float[] samples = new float[1024];
    float sum = 0.0f;

    public float loudness;

    public bool micTrigger;
    [Range(0.0f, 1f)]
    public float treshHold;

    private float counter = 0;
    [Range(0.0f, 1f)]
    public float ctrDelayTime = 0.3f;


    bool isfocussed = true;
    float noFocusTimer = 5;

    void Start()
    {
        counter = ctrDelayTime;

        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = micBus;

        mics = Microphone.devices.Length;


        
        /*debug
        for (int i = 0; i < mics; i++)
        {
            Debug.Log(Microphone.devices[i]);
        }
        */
        if (mics > 0)
        {
            selectedDevice = Microphone.devices[0].ToString();
            StopMic();
           

            StartCoroutine(StartMic());
        }
        
    }
    IEnumerator StartMic()
    {
        yield return 0;
        if (!LoadedLevel())
        {
            yield return 0;
        }

        _audioSource.clip = Microphone.Start(selectedDevice, true, 10, 48000);

        while (!(Microphone.GetPosition(selectedDevice) > 0))
        {
            _audioSource.Play();
        }
    }
    private void AnalyzeSound()
    {
        _audioSource.GetOutputData(samples, 0);

        sum = 0.0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }
        sum /= samples.Length;
        loudness = sum * 100;

    }

    private void SetTrigger()
    {
        //set counter
        if (loudness > treshHold)
        {
            counter = 0.3f;
        }
        else
        {
            counter -= (1 * Time.deltaTime); 
        }

        //set bool
        if (counter > 0.00f)
        {
            micTrigger = true;
        }
        else 
        {
            micTrigger = false;
        }
    }
    private void Update()
    {
        if (Microphone.IsRecording(selectedDevice))
        {
            AnalyzeSound();
            SetTrigger();
        }

       
        if (!isfocussed)
        {
            noFocusTimer -= 1 * Time.deltaTime;
            if(noFocusTimer < 0.0f)
            {
                //Debug.Log("bep bep auto refresh called");
                RefreshMic();
                noFocusTimer = 5;
            }
        }
    }
    void RefreshMic()
    {
        StopMic();
        StopCoroutine(StartMic());

        StartCoroutine(StartMic());
    }

    private void StopMic()
    {
        _audioSource.Stop();
        Microphone.End(selectedDevice);
    }

    public bool LoadedLevel()
    {
        if (!SceneManager.GetActiveScene().isLoaded)
        {
            return false;
        }
        return true;
    }
    
    private void OnApplicationFocus(bool focus)
    {
        //app not selected
       if(focus == false)
        {
            //activeert incremental refresh in update
            isfocussed = false;
        }
        else
        {
            RefreshMic();
            isfocussed = true;
        }
        
    }
    
}
