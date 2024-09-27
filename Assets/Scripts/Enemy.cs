using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float enemyHealth;

    public float enemyMaxHealth = 100;

    public float playerRange;

    public bool playerInRange;

    public LayerMask playerLayer;

    public EnemyHealthBar enemyHealthBar;

    public Transform Player;

    public Transform shootingPos;

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

    public bool drops;

    public GameObject healthDrop;

    bool isFacingRight;

    public AudioSource deathSound;


    void Start()
    {
        enemyHealth = enemyMaxHealth;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = startTimeBetweenShots;
        deathSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if (playerInRange)
        {
            if (Vector2.Distance(transform.position, Player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, moveSpeed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, Player.position) < stoppingDistance && Vector2.Distance(transform.position, Player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, Player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, -moveSpeed * Time.deltaTime);
            }

            if (timeBetweenShots <= 0)
            {
                Instantiate(enemyBullet, transform.position, Quaternion.identity);
                timeBetweenShots = startTimeBetweenShots;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }
        if (Player.position.x > transform.position.x)
        {
            if (!isFacingRight)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                isFacingRight = true;
            }
        }
        else if (Player.position.x < transform.position.x)
        {
            if (isFacingRight)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                isFacingRight = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            ScoreManager.AddScore(scoreAdd);
            deathSound.Play();
            Destroy(this.gameObject);
            drops = true;
        }

        if(drops)
        {
            Instantiate(healthDrop, transform.position, transform.rotation);
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            StartCoroutine(FlashYellow());
        }
    }

    public IEnumerator FlashYellow()
    {
        sprite.color = Color.yellow;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
