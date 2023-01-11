using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBar : MonoBehaviour
{
    public MicrophoneInput micInput;
    public float _loudness;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        _loudness = micInput.loudness;

        gameObject.transform.localScale = new Vector3(0.5f, _loudness, 1);
    }
}
