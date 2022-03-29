using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeLifeComponent : MonoBehaviour
{
	#region properties
	/// <summary>
	/// Sala en la que se encuentra el enemigo
	/// </summary>
	[HideInInspector] public SpongeSalaManager sala;

	/// <summary>
	/// Vida del enemigo
	/// </summary>
	public float _currentLife;

	/// <summary>
	/// Vida máxima del enemigo
	/// </summary>
	public float maxLife = 100f;

	/// <summary>
	/// ID del enemigo
	/// </summary>
	public int ID;
	#endregion

	#region parameters
	/// <summary>
	/// Booleano que indica si un enemigo está o no muerto
	/// </summary>
	public bool dead = false;
	#endregion

	#region references
	/// <summary>
	/// Referencia al Animator
	/// </summary>
	public Animator animator;

	/// <summary>
	/// Referencia al BulletLife
	/// </summary>
	private BulletLife bullet;

	/// <summary>
	/// Referencia a BubbleAttack
	/// </summary>
	private BubbleAttack bubble;

	/// <summary>
	/// Referencia a BolsaDeHielo
	/// </summary>
	private BolsaDeHielo ice;

	private BossMovement _bossMovement;
	#endregion

	#region methods
	/// <summary>
	/// Registra al enemigo en la sala actual
	/// </summary>
	public void Register()
	{
		//Debug.LogWarning("Sala" + sala);
		//Debug.LogWarning("This " + this);
		sala.RegisterEnemy(this);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		bullet = collision.gameObject.GetComponent<BulletLife>();
		bubble = collision.gameObject.GetComponent<BubbleAttack>();

		if (bullet != null) Damage(bullet.bulletDamage,true);
		if (bubble != null) Damage(bubble.bulletDamage,true);
	}

	/// <summary>
	/// Métod. para dañar a los enemigos
	/// </summary>
	/// <param name="damage">Float que contiene el daño causado al enemigo</param>
	public void Damage(float damage, bool type)
	{
		if (!dead && ( _bossMovement.agua == false|| type)) 
		{
			if (_bossMovement._direction.x == -1 && _bossMovement._direction.y == 0)
			{
				animator.SetTrigger("DIZQ");				
			}
			else if (_bossMovement._direction.x == 1 && _bossMovement._direction.y == 0)
            {
				animator.SetTrigger("DDER");
            }
			else if(_bossMovement._direction.x == 0 && _bossMovement._direction.y == 1)
            {
				animator.SetTrigger("DCULO");
            }
            else
            {
				animator.SetTrigger("DCARGA");
			}
				_currentLife -= damage;
				if (_currentLife <= 0) Die();
			
		}
		
	}

	/// <summary>
	/// Métod. que mata a los enemigos
	/// </summary>
	void Die()
	{
		//play die animation
		if (!dead)
		{
			_bossMovement._bossRB.constraints = RigidbodyConstraints2D.FreezeAll;
			animator.SetTrigger("DEAD");
			Destroy(gameObject, 2.5f);
			dead = true;
			sala.OnEnemyDies(this);
		}
	}
	#endregion

	void Start()
	{
		_currentLife = maxLife;
		_bossMovement = GetComponent<BossMovement>();
	}
}
