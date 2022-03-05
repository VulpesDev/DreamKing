using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

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
        Volume vol = GameObject.Find("Volume").GetComponent<Volume>();
        if (vol.profile.TryGet(out ColorAdjustments _colAdj))
        {
            ColorAdjustments colAdj = _colAdj;
            colAdj.postExposure.Override(gammaValue);
        }
        Character.gammaValue = gammaValue;
    }

}
