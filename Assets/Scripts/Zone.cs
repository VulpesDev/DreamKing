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
<<<<<<< HEAD
=======
        Debug.Log("In!");
>>>>>>> 5116a5dde9da69bceb361b49fe4c9a608754f9d5
        if (other.gameObject.CompareTag("Player") && !activeZone)
        {
            activeZone = true;
            transform.parent.GetComponent<ZoneManagement>().LetThereBeLight(zoneID);
        }
    }
}