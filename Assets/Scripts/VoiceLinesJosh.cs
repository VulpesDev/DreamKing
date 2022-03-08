using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLinesJosh : MonoBehaviour
{
    // line [0,4] - tutorial
    int lineCount = 0;
    AudioSource asource;
    public AudioClip[] clipsDir;
    void Start()
    {
        asource = GetComponent<AudioSource>();
        lineCount = 0;
        StartCoroutine(CheckSentences());
    }

    IEnumerator CheckSentences()
    {
        float distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
        if(!asource.isPlaying && lineCount < clipsDir.Length && distance < 7f)
        {
            if(lineCount < NemoNestCounter.counter || lineCount == 4)
            {
                StartCoroutine(PlayLine());
            }
        }
        yield return new WaitForSeconds(1.1f);
        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckSentences());
    }
    IEnumerator PlayLine()
    {
        Debug.Log(lineCount);
        asource.clip = clipsDir[lineCount];
        yield return new WaitForSeconds(1f);
        asource.Play();
        lineCount++;
    }
}

