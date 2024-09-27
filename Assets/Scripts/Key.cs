using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isFollowing;

    public float followSpeed;

    public Transform followTarget;

    public AudioSource pickUpKey;

    void Start()
    {
        
    }

    void Update()
    {
        if(isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            pickUpKey.Play();

            if(!isFollowing)
            {
                PlayerController player = FindObjectOfType<PlayerController>();

                followTarget = player.keyFollowPoint;

                isFollowing = true;

                player.followingKey = this;
            }
        }
    }
}
