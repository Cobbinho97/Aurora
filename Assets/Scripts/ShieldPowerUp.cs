using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    public GameObject shield;

    void Start()
    {
        shield.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shield.SetActive(true);
            StartCoroutine(shieldTimer());
        }
    }

    public IEnumerator shieldTimer()
    {
        yield return new WaitForSeconds(7f);
        shield.SetActive(false);
    }
}
