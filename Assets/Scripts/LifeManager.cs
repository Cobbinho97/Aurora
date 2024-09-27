using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int startingLives;

    private int lifeCounter;

    private Text lifeText;

    public GameObject gameOverScreen;

    public PlayerController player;

    void Start()
    {
        lifeText = GetComponent<Text>();

        lifeCounter = startingLives;

        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if(lifeCounter < 0)
        {
            gameOverScreen.SetActive(true);
            player.gameObject.SetActive(false);
        }

        lifeText.text = "" + lifeCounter;
    }

    public void TakeLife()
    {
        lifeCounter--;
    }

    public void AddLife()
    {
        lifeCounter++;
    }
}
