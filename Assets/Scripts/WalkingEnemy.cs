using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour
{
    public Animator hovering;

    public float moveSpeed;

    public bool moveRight;

    private Rigidbody2D enemyRB;

    public Transform wallCheck;

    public float wallCheckRadius;

    public LayerMask wallLayer;

    private bool hittingWall;

    public int scoreAdd;

    public GameObject deathEffect;

    private bool notAtEdge;

    public Transform edgeCheck;

    public AudioSource deathSound;

    void Start()
    {
       enemyRB = GetComponent<Rigidbody2D>();
       hovering = GetComponent<Animator>();
       deathSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
        notAtEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, wallLayer);

        if (hittingWall || !notAtEdge)
        {
            moveRight = !moveRight;
        }
        if(moveRight)
        {
            transform.localScale = new Vector3(-20f, 20f, 20f);
            enemyRB.velocity = new Vector2(moveSpeed, enemyRB.velocity.y);
        }    
        else
        {
            transform.localScale = new Vector3(20f, 20f, 20f);
            enemyRB.velocity = new Vector2(-moveSpeed, enemyRB.velocity.y);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            deathSound.Play();
            ScoreManager.AddScore(scoreAdd);
            Destroy(this.gameObject);
        }
    }
}
