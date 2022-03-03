using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerToSpawn : MonoBehaviour
{
    GameObject player;
    Vector3 sPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.GetComponent<Character>().enabled = false;
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = sPoint;
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<Character>().enabled = true;
        }
    }
}
