using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
	#region properties
	/// <summary>
    /// Sala en la que se encuentra el enemigo
    /// </summary>
	[HideInInspector] public SalaManager sala;

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
	/// 
	public bool _isDead;
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
    /// Métod. para dañar a los enemigos
    /// </summary>
    /// <param name="damage">Float que contiene el daño causado al enemigo</param>
	public void Damage(float damage)
	{
		animator.SetTrigger("Hurt");
		animator.SetBool("HURT", true);

		_currentLife -= damage;
		
		if (_currentLife <= 0) Die();
	}

	/// <summary>
    /// Métod. que mata a los enemigos
    /// </summary>
	void Die()
	{
		GameManager.Instance.DeadEnemies();
		_isDead = true;
		animator.SetTrigger("Die");
		animator.SetBool("DEAD", true);
		Destroy(gameObject, 1f);
		animator.SetBool("DEAD", false);
		sala.OnEnemyDies(this);
		GetComponent<BoxCollider2D>().enabled = false;
		transform.GetChild(0).GetComponentInChildren<DetectPlayer>().Deactivate();
		this.enabled = false;
	}
	#endregion

	void Start()
    {
		_currentLife = maxLife;
		_isDead = false;
    }  
}
