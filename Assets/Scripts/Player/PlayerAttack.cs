using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region references
    [SerializeField]
    private Transform shotPoint, attackPoint;
    [SerializeField]
    private GameObject bulletPrefab;
    
    // BOLSA DE HIELO (Laura)
    [SerializeField]
    private GameObject icebagPrefab;

    [SerializeField]
    private Rigidbody2D gunRB;
    
    private Animator animator;
    #endregion

    #region parameters
    public float meleeDamage = 20, shotForce = 20f, rangeDamage = 25f;

    // BOLSA DE HIELO (Laura)
    [SerializeField]
    private float iceForce = 20f;
    //public float iceDamage = 20f;
    [SerializeField]
    private LayerMask enemyLayers;

    [SerializeField]
    private float _shotCooldown = 5, _meleeCooldown = 0.2f, attackRange = 0.5f;

    private PlayerManager _playerManager;
    // Añadido por Laura
    private Transform _myTransform;
	#endregion

	#region properties
	private float _shotCooldownCounter = 0, _meleeCooldownCounter = 0;
    private Vector3 _attackPointPosition = Vector3.zero;
    // Añadido por Laura
    Vector3 mouseWorldPosition;
	#endregion

	void Start()
    {
        _playerManager = PlayerManager.Instance;
        _shotCooldownCounter = _shotCooldown;
        _meleeCooldownCounter = _meleeCooldown;
        animator = GetComponentInChildren<Animator>();
        _playerManager.UpdateMeleeDamage(meleeDamage);
        _playerManager.UpdateRangeDamage(rangeDamage);
        // Añadido por Laura
        _myTransform = transform;
    }

    void Update()
    {
        //cambiar esto a un inputManager
        if (Input.GetButtonDown("Fire1") && GetComponent<PlayerLife>().health > 0 && _shotCooldownCounter >= _shotCooldown)
		{
            Shoot();
            _shotCooldownCounter = 0;
		}
        
        if (Input.GetKeyDown(KeyCode.E) && _meleeCooldownCounter >= _meleeCooldown)
		{
            Melee();
            _meleeCooldownCounter = 0;
		}

        if (Input.GetKeyDown(KeyCode.I))
        {
            LanzaHielo();
        }

        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        _shotCooldownCounter += Time.deltaTime;
        _meleeCooldownCounter += Time.deltaTime;
        // eliminada esta visualizacion, al menos hasta revisar su funcionalidad
        //GameManager.Instance.ShowCooldown(_shotCooldownCounter, _shotCooldown);
    }

    public void SetAttackPoint(Vector3 movement)
    {
        if (movement.x > 0) _attackPointPosition.x = attackRange;
        else _attackPointPosition.x = -attackRange;
    }

    void Melee()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + _attackPointPosition, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
		{
            EnemyLifeComponent enemyLife = enemy.GetComponent<EnemyLifeComponent>();
            if (enemyLife != null) enemyLife.Damage(meleeDamage);
		}
        //Drawing things
    }
	private void OnDrawGizmosSelected()
	{
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(transform.position + _attackPointPosition, attackRange);
	}

    void Shoot()
	{
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, Quaternion.identity);
        bullet.GetComponent<BulletLife>().bulletDamage = rangeDamage;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.rotation = gunRB.rotation;
        rb.AddForce(shotPoint.right * shotForce, ForceMode2D.Impulse);
        GetComponent<PlayerLife>().SetHealth(-10);
	}

    public void SetMeleeDamage(float newMeleeDamage)
    {
        meleeDamage += newMeleeDamage;
        _playerManager.UpdateMeleeDamage(meleeDamage);
    }
    public void SetRangeDamage(float newRangeDamage)
    {
        rangeDamage += newRangeDamage;
        _playerManager.UpdateMeleeDamage(rangeDamage);
    }

    //Si tiene el objeto activo "Bolsa de Hielo", este método lo activa.
    public void LanzaHielo()
    {
        GameObject nuevoIcebagPrefab = Instantiate(icebagPrefab, shotPoint.position, Quaternion.identity);
        nuevoIcebagPrefab.GetComponent<Rigidbody2D>().AddForce(iceForce * shotPoint.right, ForceMode2D.Impulse);
    }
}