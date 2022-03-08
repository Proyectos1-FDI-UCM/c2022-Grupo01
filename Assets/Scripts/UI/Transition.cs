using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private float _transitionTime = 1f;

    private Animator transitionAnimator;

    [SerializeField] private GameObject _player;
    [SerializeField] private float position_X = 0f;
    [SerializeField] private float position_Y = 0f;
    private Transform _myTransform;

    void Start()
    {
        _myTransform = transform;
    }

    void Update()
    {
        // Teletransporte del jugador dependiendo de la posición en la que acabe la sala
        if (_myTransform.position.x == position_X && _myTransform.position.y == position_Y)
        {
            //_myTransform.position.x =;
            //_myTransform.position.y =;
        }
    }

    public void OnTriggerEnter2D (Collider2D collision)
    {
        
    }
}
