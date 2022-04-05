using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region references
    [SerializeField]
    private Transform shotPoint, attackPoint;
    [SerializeField]
    private GameObject bulletPrefab,_gun;
    
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
    private LayerMask bulletLayer;

    [SerializeField]
    private float _shotCooldown = 5, _meleeCooldown = 0.2f, attackRange = 0.5f;

    private PlayerManager _playerManager;
    private Transform _myTransform;
    private PlayerLife _playerLife;
	#endregion

	#region properties
	private float _shotCooldownCounter = 0, _meleeCooldownCounter = 0;
    private Vector3 _attackPointPosition = Vector3.zero;
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
        _myTransform = transform;
        _playerLife = GetComponent<PlayerLife>();
    }

    void Update()
    {
        //cambiar esto a un inputManager
        if (Input.GetButtonDown("Fire1") && GetComponent<PlayerLife>().health > 0 && _shotCooldownCounter >= _shotCooldown)
		{
            _gun.SetActive(true);
            Shoot();
            
            _shotCooldownCounter = 0;
		}
        
        if (Input.GetMouseButton(1) && _meleeCooldownCounter >= _meleeCooldown)
		{
            _gun.SetActive(false);
            Melee();
            _meleeCooldownCounter = 0;
		}

        mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        _shotCooldownCounter += Time.deltaTime;
        _meleeCooldownCounter += Time.deltaTime;
        // eliminada esta visualizacion, al menos hasta revisar su funcionalidad
        //GameManager.Instance.ShowCooldown(_shotCooldownCounter, _shotCooldown);
    }

    public void SetAttackPoint(Vector3 movement)
    {
        _attackPointPosition.x = attackRange * movement.x;
        _attackPointPosition.y = attackRange * movement.y;
        /*if (movement.x > 0) { _attackPointPosition.x = attackRange; _attackPointPosition.y = 0f; }
        else if (movement.x < 0) { _attackPointPosition.x = -attackRange; _attackPointPosition.y = 0f; }
        else if (movement.y > 0) { _attackPointPosition.y = attackRange; _attackPointPosition.x = 0f; }
        else if (movement.y < 0) { _attackPointPosition.y = -attackRange; _attackPointPosition.x = 0f; }
        */
    }

    void Melee()
    {
        animator.SetTrigger("Attack");
        AudioManager.Instance.Play("PlayerSweep");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + _attackPointPosition, attackRange, enemyLayers);
        Collider2D[] hitSponge = Physics2D.OverlapCircleAll(transform.position + _attackPointPosition, attackRange, bulletLayer);


        foreach (Collider2D enemy in hitEnemies)
		{
            EnemyLifeComponent enemyLife = enemy.GetComponent<EnemyLifeComponent>();
            SpongeLifeComponent bossLife = enemy.GetComponent<SpongeLifeComponent>();
            if (enemyLife != null) enemyLife.Damage(meleeDamage);
            else if (bossLife != null) bossLife.Damage(meleeDamage,false);
		}

        foreach(Collider2D spongeCollider in hitSponge)
        {
            SpongeMovement sponge = spongeCollider.GetComponent<SpongeMovement>();
            if(sponge != null)
            {
                sponge.SetMovement(_attackPointPosition);
            }
            else
            {
                // boss juan
                EnemyLifeComponent enemyLife = spongeCollider.GetComponent<EnemyLifeComponent>();
                if(enemyLife != null)
                {
                    enemyLife.Damage(meleeDamage);

                }
            }
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
        _playerManager.ChangePlayerLife(-10, true);
	}

    public void SetMeleeDamage(float newMeleeDamage)
    {
        meleeDamage += newMeleeDamage;
        _playerManager.UpdateMeleeDamage(meleeDamage);
    }
    public void SetRangeDamage(float newRangeDamage)
    {
        rangeDamage += newRangeDamage;
        _playerManager.UpdateRangeDamage(rangeDamage);
    }
}