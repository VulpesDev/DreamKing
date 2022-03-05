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
        SetGamma();
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
    float gammaValue;
    [SerializeField] Slider gammaSlider;
    public void SetGamma()
    {
        gammaValue = gammaSlider.value;
        RenderSettings.ambientLight = new Color(gammaValue, gammaValue, gammaValue, 1.0f);
        Character.brightness = gammaValue;
    }

}
