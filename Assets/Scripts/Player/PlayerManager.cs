using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region references
    public GameObject player, gun, rotationPoint, model, shotPoint, gancho;
    public Camera cam;
    
    private PlayerAttack _playerAttack;
    private PlayerLife _playerLife;
    private PlayerMovement _playerMovement;
    private Transform _playerTransform;
    private UI_Manager _uiManager;
    #endregion

    #region properties
    [HideInInspector] public bool bayeta = false;
    [HideInInspector] public int _deadEnemyCount = 0;
    public enum LifeStates { Normal, Shield, HolyFlotador };   // A�adir shields en el futuro
    public LifeStates myLifeState;
    #endregion

    #region parameters
    public float health, maxHealth, meleeDamage, rangeDamage, speed;
    public int uses;
    public Vector3 _playerPosition;
    public bool playerInRoll = false;
	#endregion

	#region Actualizar Referencias al jugador
    public void IncreaseEnemyCounter()
    {
        if (bayeta) _deadEnemyCount++;
        if(_deadEnemyCount >= 10 && bayeta)
        {
            foreach (GameObject passive in Inventory.Instance.passiveItemList)
            {
                BayetaPasivo bayeta = passive.GetComponent<BayetaPasivo>();
                if (bayeta != null) bayeta.IncreaseHealth();
            }
        }
    }

	public void UpdateLife(float playerHealth)
    {
        health = playerHealth;
    }
    public void UpdateMaxLife(float playerMaxHealth)
    {
        maxHealth = playerMaxHealth;
    }
    public void UpdateMeleeDamage(float playerMeleeDamage)
    {
        meleeDamage = playerMeleeDamage;
        _uiManager.UpdateMeleeDamage();
    }
    public void UpdateRangeDamage(float playerRangeDamage)
    {
        rangeDamage = playerRangeDamage;
        _uiManager.UpdateRangeDamage();
    }
    public void UpdateSpeed(float playerSpeed)
    {
        speed = playerSpeed;
        _uiManager.UpdateSpeed();
    }

    public void UpdatePosition()
    {
        _playerPosition = _playerTransform.position;
    }

    public void PlayerInRoll(bool inRoll)
    {
        playerInRoll = inRoll;
    }
    #endregion

    #region cambiar stats de jugador
    public void ChangePlayerLife(float health, bool isShot)
    {
        _playerLife.SetHealth(health, isShot);
    }
    public void ChangeMaxLife(int myMaxHealth)
    {
        _playerLife.SetMaxHealth(myMaxHealth);
    }
    public void ChangePlayerMeleeDamage(float newPlayerMeleeDamage)
    {
        _playerAttack.SetMeleeDamage(newPlayerMeleeDamage);
    }
    public void ChangePlayerRangeDamage(float newPlayerRangeDamage)
    {
        _playerAttack.SetRangeDamage(newPlayerRangeDamage);

    }
    public void ChangePlayerSpeed(int speed)
    {
        _playerMovement.ModifyPlayerSpeed(speed);
    }

    public void ChangePlayerShields(int shields)
    {
        _playerLife.SetShields(shields);
    }
    #endregion

    #region Singleton
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _playerAttack = player.GetComponent<PlayerAttack>();
        _playerLife = player.GetComponent<PlayerLife>();
        _playerMovement = player.GetComponent<PlayerMovement>();
        _playerTransform = player.transform;
        _uiManager = GetComponent<UI_Manager>();
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }
}