using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region references
    [SerializeField] private GameObject _controls, _button;
	#endregion

	public void LoadPlayTutorial()
    {
        //SceneManager.LoadSceneAsync("LoadingTutorial");
        SceneManager.LoadSceneAsync("Tutorial");
        GetComponent<Button>().interactable = false;
    }

    public void LoadingPlayNormalGame()
    {
        //SceneManager.LoadSceneAsync("LoadingGame");
        SceneManager.LoadSceneAsync("CompleteScene");
        GetComponent<Button>().interactable = false;
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
}
