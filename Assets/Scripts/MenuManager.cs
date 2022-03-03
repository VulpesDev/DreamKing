using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        SetSensitivity();
    }

    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    float sensValue;
    [SerializeField] Slider sensSlider;
    public void SetSensitivity()
    {
        sensValue = sensSlider.value;
        Character.lookSpeed = sensValue;
    }

}
