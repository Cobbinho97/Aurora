using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healthAmount;

    public GameObject healthEffect;

    private HealthManager healthManager;

    public AudioSource healthPickUp;

    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        healthPickUp = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            healthManager.AddHealth(healthAmount);
            healthPickUp.Play();
            Instantiate(healthEffect, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.2f);
        }
    }
}
