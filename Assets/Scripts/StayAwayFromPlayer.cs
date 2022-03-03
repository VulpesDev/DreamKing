using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayAwayFromPlayer : MonoBehaviour
{
    GameObject player;
    float diff;
    [SerializeField] float minDistance, speed;
    bool move;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        diff = 7f;
    }
    void FixedUpdate()
    {

        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist <= minDistance)
        {
            move = true;
        }
        if(dist >= diff)
        {
            move = false;
        }
        if (move)
        {
            transform.position = Vector3.Lerp(transform.position,
                new Vector3(player.transform.position.x - diff, transform.position.y, transform.position.z),
                Mathf.Clamp(dist,0.1f,1) * speed * Time.deltaTime);
        }
    }
}