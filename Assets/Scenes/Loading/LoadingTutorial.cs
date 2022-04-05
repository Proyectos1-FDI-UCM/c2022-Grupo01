using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingTutorial : MonoBehaviour
{   
    [SerializeField] private float _loadingTime = 1f;
    void Start()
    {
        StartCoroutine(this.MakeTheLoad("Tutorial"));
    }

    IEnumerator MakeTheLoad(string level)
    {
        yield return new WaitForSeconds(_loadingTime);
        SceneManager.LoadSceneAsync(level);
    }
}
