using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{

    public Animator transitionAnimation;

    public void PlayGame()
    {
        StartCoroutine(LoadScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    IEnumerator LoadScene()
    {
        transitionAnimation.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
    }
}
