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
		GameObject nuevoIcebagPrefab = Instantiate(_icebagPrefab, _shotPoint.position, Quaternion.identity);
		nuevoIcebagPrefab.GetComponent<Rigidbody2D>().AddForce(_iceForce * _shotPoint.right, ForceMode2D.Impulse);
	}
}
