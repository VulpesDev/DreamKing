using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemoNestCounter : MonoBehaviour
{
    public static int counter;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(VoiceLinesJosh.lineCount >= counter)
            counter++;
        }
    }
}
