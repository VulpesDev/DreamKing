using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public int zoneID;
    public bool activeZone = false;
    public static int maxZoneID;
    public static int curZoneID;
    private void OnTriggerExit(Collider other)
    {
        if(activeZone) activeZone = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !activeZone)
        {
            activeZone = true;
            curZoneID = zoneID;
            if(zoneID > maxZoneID) maxZoneID = zoneID;
            transform.parent.GetComponent<ZoneManagement>().LetThereBeLight(zoneID);
        }
    }
}