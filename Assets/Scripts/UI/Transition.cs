using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private float _transitionTime = 1f;

    private Animator transitionAnimator;

    public void Start()
    {
        transitionAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {   
        if (Input.anyKeyDown)
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(SceneLoad(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator SceneLoad(int sceneIndex)
    {
        //Disparar el trigger para reproducir animación FadeIn
        transitionAnimator.SetTrigger("StartTransition");
        //Esperar un segundo / lo que indique el usuario
        yield return new WaitForSeconds(_transitionTime);
        //Cargar la escena
        SceneManager.LoadScene(sceneIndex);

    }
}
