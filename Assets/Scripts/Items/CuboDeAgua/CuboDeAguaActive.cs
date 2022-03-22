using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboDeAguaActive : MonoBehaviour
{
    #region parameters
    [SerializeField] 
    private int recuperaVida = 30;
    #endregion

    #region references
    private PlayerLife _playerLife;
    #endregion

    void Start()
    {
        _playerLife = GetComponent<PlayerLife>();
    }

    #region methods
    public void PowerUpEnabled()
    {
        _playerLife.health += recuperaVida;
    }

    #endregion
}