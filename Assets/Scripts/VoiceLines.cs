using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLines : MonoBehaviour
{
    int lineCount = 0;
    AudioSource asource;
    string[] clipsDir = new string[] { "Sounds/VoiceLines/Wayne/Hey!", "Sounds/VoiceLines/Wayne/LegendaryPickle",
    "Sounds/VoiceLines/Wayne/ManyPeople", "Sounds/VoiceLines/Wayne/youDoIt", "Sounds/VoiceLines/Wayne/ActTutorial",
    "Sounds/VoiceLines/Wayne/BackInMyDay", "Sounds/VoiceLines/Wayne/WorldRecord"};
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
            asource.clip = Resources.Load<AudioClip>(clipsDir[lineCount]);
            yield return new WaitForSeconds(1f);
            asource.Play();
            lineCount++;
        }
        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckSentences());
    }
}
