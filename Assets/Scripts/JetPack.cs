using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public PlayerController playerController;

    public AudioSource pickupSound;

    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            pickupSound.Play();
            playerController.getJetpack = true;
            Destroy(this.gameObject);
        }
    }
}
