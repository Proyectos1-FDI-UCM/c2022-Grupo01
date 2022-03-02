using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaDeHieloActivo : ActiveObject
{
	[SerializeField] private GameObject _icebagPrefab;
	[SerializeField] private float _iceForce = 20f;
	private Transform _shotPoint;

	private void Start()
	{
		_shotPoint = PlayerManager.Instance.attackPoint.transform;
	}
	public override void Activate()
	{
		base.Activate();
		LanzaHielo();
	}

	void LanzaHielo()
	{
		_shotPoint = PlayerManager.Instance.attackPoint.transform;
		GameObject nuevoIcebagPrefab = Instantiate(_icebagPrefab, _shotPoint.position, Quaternion.identity);
		Debug.Log(nuevoIcebagPrefab.transform.position);
		Rigidbody2D rb = nuevoIcebagPrefab.GetComponent<Rigidbody2D>();
		rb.rotation = PlayerManager.Instance.gun.GetComponent<Rigidbody2D>().rotation;
		rb.AddForce(_shotPoint.right * _iceForce, ForceMode2D.Impulse);
	}
}
