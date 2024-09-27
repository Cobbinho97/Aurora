using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePowerUp : MonoBehaviour
{
    public GameObject lifePickUpEffect;

    private LifeManager lifeManager;

    public AudioSource pickupSound;


    void Start()
    {
        lifeManager = FindObjectOfType<LifeManager>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            pickupSound.Play();

            LifePickUp(other);
        }
    }

    void LifePickUp(Collider2D player)
    {
        Instantiate(lifePickUpEffect, transform.position, transform.rotation);

        lifeManager.AddLife();

        Destroy(gameObject);
    }
}
