using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    public GameObject levelCompleteUI;

    public PlayerController player;

    public Text finalScore;

    void Start()
    {

    }

    void Update()
    {
        finalScore.text = ScoreManager.score.ToString("0000");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        levelCompleteUI.SetActive(true);
        player.gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }
}
