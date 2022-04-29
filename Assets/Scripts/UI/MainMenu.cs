using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _controls, _button;
	#endregion

	public static void LoadPlayTutorial()
    {
        //SceneManager.LoadSceneAsync("LoadingTutorial");
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public static void LoadingPlayNormalGame()
    {
        //SceneManager.LoadSceneAsync("LoadingGame");
        SceneManager.LoadSceneAsync("CompleteScene");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ShowControls(bool showControls)
	{
        _controls.SetActive(showControls);
        _button.SetActive(true);
        gameObject.SetActive(false);
	}

	private void Start()
	{
        ShowControls(false);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            SceneManager.LoadSceneAsync("DebugMenu");
        }   
    }
}
