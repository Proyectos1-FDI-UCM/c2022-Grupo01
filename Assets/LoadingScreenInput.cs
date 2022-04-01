using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class LoadingScreenInput : MonoBehaviour
{
    #region properties
    private float _elapsedTime = 0;
    private bool activateVideo = false;
    #endregion

    #region references
    [SerializeField] private GameObject _loadingScreenText;
    #endregion

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && GetComponent<VideoPlayer>().length <= _elapsedTime)
        {
            GameManager.Instance.SetLoadingScreen(false);
            GameManager.Instance.GenerateNewFloor();
            _loadingScreenText.SetActive(false);
            _elapsedTime = 0f;
        }

        if(GetComponent<VideoPlayer>().length <= _elapsedTime && !activateVideo)
        {
            _loadingScreenText.SetActive(true);
            activateVideo = true;
        }
    }
}
