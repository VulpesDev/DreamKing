using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTick : MonoBehaviour
{
    Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        StartCoroutine(CheckRandom());
    }

    IEnumerator CheckRandom()
    {
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            StartCoroutine(StartAnime());
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(CheckRandom());
    }
    IEnumerator StartAnime()
    {
        anime.SetBool("Tick", true);
        yield return new WaitForEndOfFrame();
        anime.SetBool("Tick", false);
    }
}
