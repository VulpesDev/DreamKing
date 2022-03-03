using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds
{

    public static void PlaySound(string type, float volume)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Footstep1");
        switch (type)
        {
            case "Footstep1":
                clip = Resources.Load<AudioClip>("Sounds/Player/Footstep1");
                break;
            case "Footstep2":
                clip = Resources.Load<AudioClip>("Sounds/Player/Footstep2");
                break;
            case "Jump":
                clip = Resources.Load<AudioClip>("Sounds/Player/Jump");
                break;
            case "Bounce":
                clip = Resources.Load<AudioClip>("Sounds/Player/Bounce");
                break;
        }
        GameObject as_go = new GameObject();
        AudioSource as_as = as_go.AddComponent<AudioSource>();
        as_as.clip = clip; as_as.Play(); as_go.AddComponent<DestroyAfterMusic>();
        as_as.volume = volume;
        GameObject.Instantiate(as_go);
    }
}
public class DestroyAfterMusic : MonoBehaviour
{
    AudioSource as_as;
    private void Start()
    {
        as_as = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (!as_as.isPlaying) Destroy(gameObject);
    }
}
