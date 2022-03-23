using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void LoadPlayTutorial()
    {
        SceneManager.LoadScene("LoadingTutorial");
    }

    public static void LoadingPlayNormalGame()
    {
        SceneManager.LoadScene("LoadingGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
