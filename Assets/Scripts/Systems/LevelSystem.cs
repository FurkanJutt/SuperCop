using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    int currentSceneIndex = 0;

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (currentSceneIndex == 0)
        {
            yield return new WaitForSeconds(4f);
            LoadStartMenu();
        }
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("OptionsScreen");
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
