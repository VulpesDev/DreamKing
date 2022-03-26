using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Appear : MonoBehaviour
{
    [SerializeField] GameObject light2;
    bool once = false;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        float dist = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
        if(StartVoice.ready && GetComponent<Renderer>().isVisible && dist <= 3 && !once)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().canMove = false;
            GameObject.Find("Music").GetComponent<DontDestroy>().StartScreetch();
            Invoke("GetReadyForTransfer", 3f);
            once = true;
        }
    }
    void GetReadyForTransfer()
    {
        light2.SetActive(false);
        Invoke("ChangeScene", 0.2f);
    }
    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
