using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMove : MonoBehaviour
{

    private PlayerController playerController;

    public float enemyMoveSpeed;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInRange;

    public float enemyHealth;

    public float enemyMaxHealth = 50;

    public GameObject deathEffect;

    public int scoreAdd;

    public SpriteRenderer sprite;

    public Transform Player;

    bool isFacingRight;

    public AudioSource deathSound;


    void Start()
    {
        enemyHealth = enemyMaxHealth;
        playerController = FindObjectOfType<PlayerController>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        deathSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if (playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, enemyMoveSpeed * Time.deltaTime);
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

        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            deathSound.Play();
            ScoreManager.AddScore(scoreAdd);
            Destroy(this.gameObject);
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
