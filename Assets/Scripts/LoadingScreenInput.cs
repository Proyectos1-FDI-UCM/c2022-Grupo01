using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
    
public class LoadingScreenInput : MonoBehaviour
{
    #region properties
    private float _elapsedTime = 0;
    private bool activateVideo = false;
    enum ScreenType { Tutorial, Game, DuringGame }
    #endregion

    #region parameters
    [SerializeField] private ScreenType _screenType;
	#endregion

	#region references
	[SerializeField] private GameObject _loadingScreenText;
    #endregion

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && activateVideo && _screenType == ScreenType.DuringGame)
        {
            GameManager.Instance.SetLoadingScreen(false);
            _elapsedTime = 0f;
            _loadingScreenText.SetActive(false);

            GameManager.Instance.GenerateNewFloor();
        }

        else if (Input.GetKeyDown(KeyCode.Space) && activateVideo && _screenType == ScreenType.Game)
		{
            SceneManager.LoadSceneAsync("CompleteScene");
		}
        else if (Input.GetKeyDown(KeyCode.Space) && activateVideo && _screenType == ScreenType.Tutorial)
		{
            SceneManager.LoadSceneAsync("Tutorial");
        }

        if (5 <= _elapsedTime && !activateVideo)
        {
            _loadingScreenText.SetActive(true);
            activateVideo = true;
        }
    }
}