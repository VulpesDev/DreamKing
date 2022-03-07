using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManagement : MonoBehaviour
{
    private LanternID[] lanterns;

    void Start()
    {
        GameObject[] lanternsGO = GameObject.FindGameObjectsWithTag("Lantern");
        lanterns = new LanternID[lanternsGO.Length];
        IComparer myComparer = new Comparer();
        Array.Sort(lanternsGO, myComparer);
        for (int i = 0; i < lanternsGO.Length; i++)
        {
            lanterns[i] = lanternsGO[i].GetComponent<LanternID>();
        }
    }
    public void LetThereBeLight(int zoneID)
    {
        for (int i = 0; i < lanterns.Length; i++)
        {
            if(i != zoneID - 1 && i != zoneID && lanterns[i].lit)
            lanterns[i].TurnLightOff();
        }
        //if (zoneID >= 1 && zoneID < lanterns.Length)
        //    lanterns[zoneID - 1].TurnLightOff();
        if (!lanterns[zoneID].lit)
        lanterns[zoneID].TurnLightOn();

    }
}
public class Comparer : IComparer
{

    // Calls CaseInsensitiveComparer.Compare on the monster name string.
    int IComparer.Compare(System.Object x, System.Object y)
    {
        return ((new CaseInsensitiveComparer()).Compare(((GameObject)x).name, ((GameObject)y).name));
    }

}
