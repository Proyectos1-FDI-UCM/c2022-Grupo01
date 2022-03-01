using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    public float maxLife = 100f;
	public Animator animator;

    public float _currentLife;

	private BulletLife bullet;
	private BubbleAttack bubble;
	private BolsaDeHielo ice;
	
	private void Start()
	{
		_currentLife = maxLife;
		animator = GetComponent<Animator>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		bullet = collision.gameObject.GetComponent<BulletLife>();
		bubble = collision.gameObject.GetComponent<BubbleAttack>();
		ice = collision.gameObject.GetComponent<BolsaDeHielo>();

		if (bullet != null)
		{
			Damage(bullet.bulletDamage);
		}
		if (bubble != null)
		{
			Damage(bubble.bulletDamage);
		}
		if (ice != null)
		{
			Damage(ice.iceDamage);
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
		animator.SetTrigger("Die");
		Destroy(gameObject, 1f);
	}
}
