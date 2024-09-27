using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;

    private PlayerController player;

    public HealthManager healthManager;

    public GameObject playerdeathEffect;

    public int pointPenalty;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        healthManager = FindObjectOfType<HealthManager>();
    }

    public void RespawnPlayer()
    {
        Debug.Log("Player Respawn");
        Instantiate(playerdeathEffect, player.transform.position, player.transform.rotation);
        ScoreManager.AddScore(-pointPenalty);
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        player.transform.position = currentCheckpoint.transform.position;
        healthManager.FullHealth();
        healthManager.isDead = false;
    }
}
