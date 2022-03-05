using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartScreetch()
    {
        gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
    }
}
