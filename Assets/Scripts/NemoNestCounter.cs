using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemoNestCounter : MonoBehaviour
{
    public static bool passed;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            passed = true;
            VoiceLinesJosh.introLinesB = true;
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            passed = true;
            VoiceLinesJosh.introLinesB = true;
        }
    }
#endif
}
