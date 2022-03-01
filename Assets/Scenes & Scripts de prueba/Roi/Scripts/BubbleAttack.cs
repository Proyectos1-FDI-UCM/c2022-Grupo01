using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAttack : MonoBehaviour
{
	public float lifeTime = 2f;
	public float bulletDamage = 30f;
	public GameObject hitEffect;
	public Transform hitSpawn;
	public Rigidbody2D bulletRB;
	[HideInInspector]
	public PomperActiveObject pompero;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		EnemyLifeComponent _myEnemyLifeComponent = collision.gameObject.GetComponent<EnemyLifeComponent>();
		if(_myEnemyLifeComponent != null)
        {
			ContactPoint2D punto = collision.GetContact(0);
			GameObject effect = Instantiate(hitEffect, punto.point, Quaternion.identity);
			Destroy(effect, 2f);
			pompero._bubbleActive = false;
			Destroy(gameObject);
		}
	}
}
