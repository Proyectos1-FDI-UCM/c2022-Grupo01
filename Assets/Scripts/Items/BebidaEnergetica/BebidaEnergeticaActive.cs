using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BebidaEnergeticaActive : MonoBehaviour
{
    // Cuando el jugador tenga el objeto se activa este script que cogerá
    // el componente PlayerAttack del jugador
    #region parameters
    [SerializeField] private float _coolDown = 15f;
    private bool speedUp;
    [SerializeField] private int speedBoost = 20;
    #endregion

    #region references
    private PlayerAttack _playerAttack;
    private PlayerMovement _playerMovement;
    #endregion

    void Start()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    #region methods
    // Una vez que el jugador toque el objeto/le de a la tecla de activar objeto, llamar a este método para el aumento de velocidad
    public void PowerUpEnabled()
    {
        // AUMENTO VELOCIDAD
        // =================
        speedUp = true;
        speed *= speedBoost;

        // AUMENTO DAÑO
        // ============
        // Aumenta el daño melee (+5PV)
        _playerAttack.meleeDamage += 5f;

        // Aumenta el daño a rango (+5PV)
        _playerAttack.rangeDamage += 5f;

        StartCoroutine(PowerUpDisableRoutine());
    }

    IEnumerator PowerUpDisableRoutine()
    {
        yield return new WaitForSeconds(_coolDown);

        // DISMINUYO TODO
        // ==============
        speed /= speedBoost;
        _playerAttack.meleeDamage -= 5f;
        _playerAttack.rangeDamage -= 5f;
    }
    #endregion
}
