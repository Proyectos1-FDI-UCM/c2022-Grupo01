using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _target;

    public void OnTriggerEnter2D (Collider2D collision)
    {
        _player.transform.position = _target.transform.position;
    }
}
