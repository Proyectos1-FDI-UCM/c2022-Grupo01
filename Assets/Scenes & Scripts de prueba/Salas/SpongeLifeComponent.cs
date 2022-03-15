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
	/// Vida m�xima del enemigo
	/// </summary>
	public float maxLife = 100f;

	/// <summary>
	/// ID del enemigo
	/// </summary>
	public int ID;
	#endregion

	#region parameters
	/// <summary>
	/// Booleano que indica si un enemigo est� o no muerto
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

		if (bullet != null) Damage(bullet.bulletDamage);
		if (bubble != null) Damage(bubble.bulletDamage);
	}

	/// <summary>
	/// M�tod. para da�ar a los enemigos
	/// </summary>
	/// <param name="damage">Float que contiene el da�o causado al enemigo</param>
	public void Damage(float damage)
	{
		if (!dead)
		{
			animator.SetTrigger("Hurt");
			_currentLife -= damage;
			if (_currentLife <= 0) Die();
		}
	}

	/// <summary>
	/// M�tod. que mata a los enemigos
	/// </summary>
	void Die()
	{
		//play die animation
		if (!dead)
		{
			animator.SetTrigger("Die");
			Destroy(gameObject, 1f);
			dead = true;
			sala.OnEnemyDies(this);
		}
	}
	#endregion

	void Start()
	{
		_currentLife = maxLife;
	}
}
