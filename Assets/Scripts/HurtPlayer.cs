using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public LevelManager levelManager;

    public AudioSource hurtSound;

    public int damageToGive;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        hurtSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            HealthManager.HurtPlayer(damageToGive);
            hurtSound.Play();
        }
    }
}
