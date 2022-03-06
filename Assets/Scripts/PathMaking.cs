using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaking : MonoBehaviour
{
    public LanternID[] lanterns;
    LanternID closestLantern;
    LanternID lastClosestLantern;
    int closestLanternID;
    int lastClosestLanternID;

    GameObject player;

    float updateRate = 1f;
    void Start()
    {
        Invoke("DelayedStart", 1f);
    }
    void DelayedStart()
    {
        closestLantern = lanterns[0];
        closestLanternID = 0;
        lastClosestLantern = lanterns[0];
        lastClosestLanternID = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(CheckForLanterns());
    }
    IEnumerator CheckForLanterns()
    {
        for (int i = 0; i < lanterns.Length; i++)
        {
            if(Vector3.Distance(player.transform.position, lanterns[i].gameObject.transform.position) < 
                Vector3.Distance(player.transform.position, closestLantern.gameObject.transform.position))
            {
                closestLantern = lanterns[i];
                closestLanternID = i;
            }
        }
        if(lastClosestLantern != closestLantern)
        {
            if (lastClosestLanternID != 0)
            {
                lanterns[lastClosestLanternID - 1].TurnLightOff();
            }

            lastClosestLantern.TurnLightOff();
            closestLantern.TurnLightOn();

            if (closestLanternID != 0)
            {
                lanterns[closestLanternID - 1].TurnLightOn();
            }

            lastClosestLantern = closestLantern;
            lastClosestLanternID = closestLanternID;
        }
        yield return new WaitForSeconds(updateRate);
        StartCoroutine(CheckForLanterns());
    }
}
