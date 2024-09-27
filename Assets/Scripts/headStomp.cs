using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headStomp : MonoBehaviour
{
    public float bounceOnEnemy;

    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, bounceOnEnemy);
        }

        if(other.tag == "Blade")
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, bounceOnEnemy);
        }
    }
}
