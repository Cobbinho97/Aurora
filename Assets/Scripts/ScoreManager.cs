using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    Text text;

    public int highScore;

    void Start()
    {
        text = GetComponent<Text>();

        score = 0;
    }

    void Update()
    {
        if (score < 0)
        {
            score = 0;
        }

        text.text = "000" + score;
    }

    public static void AddScore(int scoreAdd)
    {
        score += scoreAdd;
    }

    public static void Reset()
    {
        score = 0;
    }
}
