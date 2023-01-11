using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public MicrophoneInput micInput;
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
        render.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (micInput.micTrigger)
        {
            render.enabled = true; 
        }
        else
        {
            render.enabled = false;
        }
    }
}
