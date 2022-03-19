using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedRun_Result : MonoBehaviour
{
    public TextMeshProUGUI resultTXT;
    void Start()
    {
        resultTXT.gameObject.SetActive(SpeedRun_Menu.value);
        this.enabled = SpeedRun_Menu.value;
        resultTXT.text = $"{TimerSR.minutes.ToString("00")}:{TimerSR.seconds.ToString("00")}:{TimerSR.microseconds.ToString("00")}";
    }

    void Update()
    {
        
    }
}
