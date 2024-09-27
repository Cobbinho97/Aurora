using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderArea : MonoBehaviour
{
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.name == "Player")
        {
            player.onLadder = true;

        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            player.onLadder = false;

        }
    }
}
