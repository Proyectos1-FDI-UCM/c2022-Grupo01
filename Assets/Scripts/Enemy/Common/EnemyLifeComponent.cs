using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeComponent : MonoBehaviour
{
    public float maxLife = 100f;
	public Animator animator;
	//[HideInInspector]
	public SalaManager sala;

    public float _currentLife;

	private BulletLife bullet;
	private BubbleAttack bubble;
	private BolsaDeHielo ice;
	public bool dead = false;
    
   
	public void Register()
	{
		Debug.LogWarning("Sala" + sala);
		Debug.LogWarning("This " + this);
		sala.RegisterEnemy(this);
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
		if (!dead)
		{
			animator.SetTrigger("Hurt");
			_currentLife -= damage;
			if (_currentLife <= 0)
				Die();
		}
	}

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
}
