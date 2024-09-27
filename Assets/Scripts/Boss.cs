using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Animator bossAnimation;

    public AudioSource deathSound;

    public float bossHealth;

    public float bossMaxHealth = 500;

    public Slider bossHealthBar;

    public GameObject bosshealthBar;

    public Transform Player;

    public GameObject deathEffect;

    private Vector2 movement;

    public float moveSpeed;

    public float stoppingDistance;

    public float retreatDistance;

    private float timeBetweenShots;

    public float startTimeBetweenShots;

    public GameObject enemyBullet;

    public SpriteRenderer sprite;

    public int scoreAdd;

    public Transform shootingPosition;

    public float playerRange;

    public bool playerInRange;

    public LayerMask playerLayer;

    void Start()
    {
        bossHealth = bossMaxHealth;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = startTimeBetweenShots;
        bossAnimation = GetComponent<Animator>();
        deathSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);
        bossHealthBar.value = bossHealth;

        if (playerInRange)
        {
            if (timeBetweenShots <= 0)
            {
                Instantiate(enemyBullet, shootingPosition.position, Quaternion.identity);
                timeBetweenShots = startTimeBetweenShots;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        bossHealth -= damage;

        if (bossHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            deathSound.Play();
            ScoreManager.AddScore(scoreAdd);
            Destroy(this.gameObject);
            bosshealthBar.SetActive(false);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            StartCoroutine(FlashRed());
        }
    }

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
