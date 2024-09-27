using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int playerHealth;

    public int maxPlayerHealth;

    private LevelManager levelManager;

    public Slider slider;

    public Gradient gradient;

    public Image fill;

    public bool isDead;

    public HealthBar healthBar;

    private LifeManager lifeManager;

    public GameObject playerdeathEffect;

    public AudioSource deathSound;

    void Start()
    {
        playerHealth = maxPlayerHealth;

        healthBar.SetMaxHealth(maxPlayerHealth);

        levelManager = FindObjectOfType<LevelManager>();

        lifeManager = FindObjectOfType<LifeManager>();

        deathSound = GetComponent<AudioSource>();

        isDead = false;
    }

    void Update()
    {
        slider.value = playerHealth;
        healthBar.SetHealth(playerHealth);

        if(playerHealth <= 0 && !isDead)
        {
            lifeManager.TakeLife();
            deathSound.Play();
            levelManager.RespawnPlayer();
            isDead = true;
        }
    }

    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
    }

    public void AddHealth(int health)
    {
        playerHealth += health;
        if(playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }
        slider.value = playerHealth;
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }
}
