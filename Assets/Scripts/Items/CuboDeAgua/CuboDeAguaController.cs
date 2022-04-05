using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboDeAguaController : MonoBehaviour
{
    #region parameters
    [HideInInspector] public int recuperaVida = 30;
    [HideInInspector] public float _cooldown = 40f;
    [HideInInspector] public float _elapsedTime = 0f;
    #endregion

    #region references
    private PlayerManager _playerManager;
    #endregion

    void Start()
    {
        _playerManager = PlayerManager.Instance;
        Heal();
    }

	private void Update()
	{
        _elapsedTime += Time.deltaTime;
        GameManager.Instance.ShowActiveCooldown(_elapsedTime, _cooldown);
        if (Input.GetKeyDown(KeyCode.Q) && _elapsedTime >= _cooldown) Heal();
    }

	#region methods
	public void Heal()
    {
        int sobraVida = ((int)_playerManager.health + recuperaVida) - (int)_playerManager.maxHealth;
        _playerManager.ChangePlayerLife(recuperaVida, false);
        Debug.Log(sobraVida);
        if(sobraVida >= 0)recuperaVida = sobraVida;
    }
    #endregion
}