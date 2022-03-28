using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BebidaEnergeticaController : MonoBehaviour
{
    // Cuando el jugador tenga el objeto se activa este script que cogerá
    // los componentes PlayerAttack y PlayerAttack del jugador
    #region parameters
    [HideInInspector] public float _cooldown = 15f;
    [HideInInspector] public int speedBoost = 2;
    #endregion

    #region properties
    private float _elapsedTime;
    #endregion

    #region references
    private PlayerManager _playerManager;
    #endregion

    void Start()
    {
        _playerManager = PlayerManager.Instance;
        StartCoroutine(PowerUp());
    }


    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        GameManager.Instance.ShowActiveCooldown(_elapsedTime, _cooldown);
        if (Input.GetKeyDown(KeyCode.Q) && _elapsedTime >= _cooldown) StartCoroutine(PowerUp());
    }

    #region methods
    IEnumerator PowerUp()
    {
        _elapsedTime = 0;
        _playerManager.ChangePlayerSpeed(speedBoost);
        _playerManager.ChangePlayerMeleeDamage(5);
        _playerManager.ChangePlayerRangeDamage(5);

        yield return new WaitForSeconds(_cooldown/4);

        _playerManager.ChangePlayerSpeed(-speedBoost);
        _playerManager.ChangePlayerRangeDamage(-5);
        _playerManager.ChangePlayerMeleeDamage(-5);
    }
    #endregion
}
