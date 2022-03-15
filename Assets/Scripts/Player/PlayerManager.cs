using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region references
    public GameObject player, gun, rotationPoint, model, shotPoint, gancho;
    
    private Animator _animator;

    private PlayerAttack _playerAttack;
    private PlayerLife _playerLife;
    private PlayerMovement _playerMovement;
    private Transform _playerTransform;
    #endregion

    #region properties
    public enum LifeStates { Normal, Shield, HolyFlotador };   // A�adir shields en el futuro
    //[HideInInspector]
    public LifeStates myLifeState;
    #endregion

    #region parameters
    public float health, maxHealth, meleeDamage, rangeDamage, speed;
    public int uses;
    public Vector3 _playerPosition;
    public bool playerInRoll = false;

    [SerializeField] public float _previusCooldown = -2;
	#endregion

	#region Actualizar Referencias al jugador
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
    }
    public void UpdateRangeDamage(float playerRangeDamage)
    {
        rangeDamage = playerRangeDamage;
    }
    public void UpdateSpeed(float playerSpeed)
    {
        speed = playerSpeed;
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
    public void ChangePlayerLife(float health)
    {
        _playerLife.SetHealth(health, false);  
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
        _animator = model.GetComponent<Animator>();
        _playerAttack = player.GetComponent<PlayerAttack>();
        _playerLife = player.GetComponent<PlayerLife>();
        _playerMovement = player.GetComponent<PlayerMovement>();
        _playerTransform = player.transform;
        _previusCooldown = -2;
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }
}