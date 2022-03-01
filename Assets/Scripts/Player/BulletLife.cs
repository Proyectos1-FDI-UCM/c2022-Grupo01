using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLife : MonoBehaviour
{
    public float lifeTime = 2f;
    public float bulletDamage = 30f;
	public GameObject hitEffect;
	public Transform hitSpawn;
	public Rigidbody2D bulletRB;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		ContactPoint2D punto = collision.GetContact(0);
		GameObject effect = Instantiate(hitEffect, punto.point, Quaternion.identity);
		Destroy(effect, 2f);
		PlayerLife playerLife = collision.gameObject.GetComponent<PlayerLife>();
		if (playerLife != null) playerLife.SetHealth(-bulletDamage);
		Destroy(gameObject);
	}
}
