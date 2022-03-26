using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboDeAguaActive : MonoBehaviour
{
    #region parameters
    [HideInInspector] public int recuperaVida = 30;
    #endregion

    #region references
    private PlayerManager _playerManager;
    #endregion

    void Start()
    {
        _playerManager = PlayerManager.Instance;
    }

    #region methods
    public void Heal()
    {
        int sobraVida = ((int)_playerManager.health + recuperaVida) % (int)_playerManager.maxHealth;
        _playerManager.ChangePlayerLife(recuperaVida);
        recuperaVida = sobraVida;
    }
    #endregion
}