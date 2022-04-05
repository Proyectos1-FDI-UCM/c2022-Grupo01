using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void LoadPlayTutorial()
    {
        SceneManager.LoadSceneAsync("LoadingTutorial");
    }

    public static void LoadingPlayNormalGame()
    {
        SceneManager.LoadSceneAsync("LoadingGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
