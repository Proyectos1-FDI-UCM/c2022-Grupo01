using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingGame : MonoBehaviour
{
    [SerializeField] private float _loadingTime = 1f;
    void Start()
    {
        StartCoroutine(MakeTheLoad("CompleteScene"));
    }
    
    IEnumerator MakeTheLoad(string level)
    {
        yield return new WaitForSeconds(_loadingTime);

        SceneManager.LoadScene(level);
    }
}
