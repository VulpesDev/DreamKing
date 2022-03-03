using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    Vector3 currentPos, lastPos;
    [SerializeField] float stepDist;
    float distanceCovered;
    int times = 0;
    void Start()
    {
        times = 0;
        currentPos = transform.position;
        lastPos = currentPos;
    }
    void Update()
    {
        currentPos = transform.position;
        Vector3 distanceVector = currentPos - lastPos;
        distanceCovered += distanceVector.magnitude;
        lastPos = currentPos;

        if (GetComponent<Character>().grounded)
        {
            if (distanceCovered > stepDist)
            {
                distanceCovered = 0;
                switch(times)
                {
                    case 0:
                        Sounds.PlaySound("Footstep1", 0.35f);
                        times++;
                        break;
                    case 1:
                        Sounds.PlaySound("Footstep2", 0.35f);
                        times--;
                        break;
                }
                
            }
        }
    }
}
