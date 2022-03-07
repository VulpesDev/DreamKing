using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternID : MonoBehaviour
{
    Renderer _renderer;
    Vector4 startColor;
    MaterialPropertyBlock _propBlock;

    public bool lit;
    void Start()
    {
        _renderer = transform.GetChild(0).GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();
        startColor = _renderer.material.GetColor("_Color");

        TurnLightOff();
    }

    public void TurnLightOff()
    {
        lit = false;

        //change emission intensity
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_EmissionColor", startColor * 0f);
        _renderer.SetPropertyBlock(_propBlock);

        //turn off the sound
        transform.GetChild(0).GetComponent<AudioSource>().Stop();
        //turn off the light
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void TurnLightOn()
    {
        lit = true;

        GetComponent<AudioSource>().Play();

        //change emission intensity
        _renderer.GetPropertyBlock(_propBlock);
        _propBlock.SetColor("_EmissionColor", startColor * 2.6f);
        _renderer.SetPropertyBlock(_propBlock);

        //turn on the sound
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        //turn on the light
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
