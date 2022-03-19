using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpeedRun_Level : MonoBehaviour
{
    float timePassed, timeTillNow;
    public GameObject josh, wayne, timerUI;
    // Start is called before the first frame update
    void Start()
    {
        if(SpeedRun_Menu.value)
        {
            josh.SetActive(false);
            wayne.SetActive(false);
            timerUI.SetActive(true);
            timeTillNow = Time.time;
        }
        else
        {
            timerUI.SetActive(false);
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timePassed = Time.time - timeTillNow;
        TimerSR.minutes = MathF.Floor(timePassed / 60);
        TimerSR.seconds = MathF.Round(timePassed - TimerSR.minutes * 60, 0);
        TimerSR.microseconds = MathF.Round((timePassed % 1) * 100, 0);
        if (TimerSR.microseconds == 100) TimerSR.microseconds = 99;
        timerUI.GetComponent<TextMeshProUGUI>().text = 
            $"{TimerSR.minutes.ToString("00")}:{TimerSR.seconds.ToString("00")}:{TimerSR.microseconds.ToString("00")}";
    }
}
