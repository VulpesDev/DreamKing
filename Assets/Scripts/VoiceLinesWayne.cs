using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLinesWayne : MonoBehaviour
{
    // line [0,4] - tutorial
    int lineCount = 0;
    AudioSource asource;
    public AudioClip[] clipsDir;
    public AudioClip[] randomClips;

    bool fallPotential;
    void Start()
    {
        asource = GetComponent<AudioSource>();
        lineCount = 0;
        StartCoroutine(CheckSentences());
    }

    private void FixedUpdate()
    {
        if(Zone.curZoneID > 2)
        fallPotential = true;
    }
    IEnumerator CheckSentences()
    {
        float distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
        if(!asource.isPlaying  && distance < 8f)
        {
            if (lineCount >= 0 && lineCount <= 4 && lineCount < clipsDir.Length)
            {
                StartCoroutine(PlayLine());
            }
            else if (Zone.maxZoneID >= 6 && lineCount <= 5 && lineCount < clipsDir.Length)
            {
                StartCoroutine(PlayLine());
            }
            else if (Zone.maxZoneID >= 6 && lineCount <= 6 && lineCount < clipsDir.Length)
            {
                StartCoroutine(PlayLine());
            }
            else if (Zone.maxZoneID >= 8 && lineCount <= 7 && lineCount < clipsDir.Length)
            {
                StartCoroutine(PlayLine());
            }
            else if (Zone.maxZoneID >= 8 && lineCount <= 8 && lineCount < clipsDir.Length)
            {
                StartCoroutine(PlayLine());
            }
            else if(fallPotential)
            {
                asource.clip = randomClips[Random.Range(0, randomClips.Length)];
                asource.Play();
                fallPotential = false;
            }
            
        }
        yield return new WaitForSeconds(1.1f);
        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckSentences());
    }
    IEnumerator PlayLine()
    {
        asource.clip = clipsDir[lineCount];
        yield return new WaitForSeconds(1f);
        asource.Play();
        lineCount++;
    }
}

