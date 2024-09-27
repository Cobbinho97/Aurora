using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private PlayerController player;

    public SpriteRenderer sprite;

    public bool doorOpen, waitingToOpen;

    public AudioSource doorOpenSound;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        doorOpenSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(waitingToOpen)
        {
            if(Vector3.Distance(player.followingKey.transform.position, transform.position) <0.1f)
            {
                waitingToOpen = false;
                doorOpen = true;
                player.followingKey = null;
                player.followingKey.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(player.followingKey != null)
            {
                player.followingKey.followTarget = transform;
                waitingToOpen = true;
                doorOpenSound.Play();
                this.gameObject.SetActive(false);
                player.followingKey.gameObject.SetActive(false);
            }
        }
    }
}
