using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedRun_Menu : MonoBehaviour
{
    public static bool value = false;

    public void ValueChanged(bool toggle)
    {
        value = toggle;
    }
}
