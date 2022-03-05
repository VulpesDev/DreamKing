using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVoice : MonoBehaviour
{
    AudioSource aus;
    public static bool ready;
    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponent<AudioSource>();
        ready = false;
        Invoke("Voice", 2.5f);
    }

    void Voice()
    {
        aus.Play();
        Invoke("Ready", 5f);
    }
    void Ready()
    {
        ready = true;
    }
}
