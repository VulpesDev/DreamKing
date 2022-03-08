using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int zoneID;
    public bool activeZone = false;
    private void OnTriggerExit(Collider other)
    {
        if(activeZone) activeZone = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !activeZone)
        {
            activeZone = true;
            transform.parent.GetComponent<ZoneManagement>().LetThereBeLight(zoneID);
        }
    }
}