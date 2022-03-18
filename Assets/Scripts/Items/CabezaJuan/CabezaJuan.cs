using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabezaJuan : MonoBehaviour
{
    #region parameters
    private bool speedUp;
    [SerializeField] private float speedBoost = 1.1f;
    #endregion

    #region references
    private PlayerMovement _playerMovement;
    #endregion

    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    #region methods
    public void PowerUpEnabled()
    {
        speedUp = true;
        //_playerMovement.movementSpeed *= speedBoost;
    }

    #endregion
}
