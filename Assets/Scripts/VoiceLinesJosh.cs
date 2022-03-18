using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLinesJosh : MonoBehaviour
{
    public AudioClip[] clipsTutorial;
    public AudioClip[] introDuction;
    public AudioClip[] randomClips;
    public AudioClip theWayBack;
    public AudioClip theWrongWayBack;
    AudioSource asource;
    bool wrongWay;
    int introLines = 0;
    int tutorialLines = 0;
    public static bool introLinesB;
    bool theWayBackTutorial;

    public Light lanternLight;
    bool sound = false;

    private void Start()
    {
        asource = GetComponent<AudioSource>();
        distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
        StartCoroutine(CustomUpdate());
    }
    float distance;
    private void FixedUpdate()
    {
        distance = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
        lanternLight.enabled = distance <= 10f; 
        if(lanternLight.enabled && !sound)
        {
            lanternLight.gameObject.GetComponent<AudioSource>().Play();
            sound = true;
        }
        else if (!lanternLight.enabled)
            sound = false;
    }

    IEnumerator CustomUpdate()
    {
        if (distance <= 7f && !asource.isPlaying)
        {
            if (NemoNestCounter.passed)
            {
                if (introLines < introDuction.Length)
                {
                    if (introLines == 1 && !theWayBackTutorial)
                    {
                        theWayBackTutorial = true;
                        StartCoroutine(PlayLine(theWayBack));
                    }
                    else
                    {
                        if (introLinesB)
                        {
                            StartCoroutine(PlayLine(introDuction[introLines]));
                            introLines++;
                            introLinesB = false;
                        }

                    }
                }
                else
                {
                    if (Zone.maxZoneID < 12 && introLinesB && tutorialLines < clipsTutorial.Length)
                    {
                        StartCoroutine(PlayLine(clipsTutorial[tutorialLines]));
                        tutorialLines++;
                    }
                    else if (introLinesB)
                    {
                        int r = Random.Range(0, randomClips.Length);
                        StartCoroutine(PlayLine(randomClips[r]));
                        introLinesB = false;
                    }
                }
            }
            else
            {
                if (!wrongWay && !asource.isPlaying)
                {
                    wrongWay = true;
                    StartCoroutine(PlayLine(theWrongWayBack));
                }
            }
        }

        yield return new WaitForSeconds(1.1f);
        StartCoroutine(CustomUpdate());
    }
    IEnumerator PlayLine(AudioClip clip)
    {
        asource.clip = clip;
        yield return new WaitForSeconds(0.5f);
        asource.Play();
    }
}

