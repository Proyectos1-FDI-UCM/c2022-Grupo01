using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    public float maxLife = 250f;
	public Animator animator;
	public SalaManager sala;

    public float _currentLife;

	private BulletLife bullet;
	private BubbleAttack bubble;
	private BolsaDeHielo ice;
	public bool dead = false;
    
    private void Start()
	{
		_currentLife = maxLife;
		animator = GetComponent<Animator>();
		sala.GetComponentInParent<SalaManager>();
		sala.RegisterEnemy();
		
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		bullet = collision.gameObject.GetComponent<BulletLife>();
		bubble = collision.gameObject.GetComponent<BubbleAttack>();

		if (bullet != null)
		{
			Damage(bullet.bulletDamage);
		}
		if (bubble != null)
		{
			Damage(bubble.bulletDamage);
		}
		
	}

	public void Damage(float damage)
	{
		animator.SetTrigger("Hurt");
		_currentLife -= damage;
		if (_currentLife <= 0)
			Die();
	}

	void Die()
	{
		//play die animation
		if (!dead)
		{
			animator.SetTrigger("Die");
			Destroy(gameObject, 1f);
			dead = true;
			sala.OnEnemyDies(); //lo contea como uno mas
		}
	}
}
