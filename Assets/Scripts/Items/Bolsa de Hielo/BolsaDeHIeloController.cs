using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaDeHieloController : MonoBehaviour
{
    #region parameters
	[SerializeField] private float _iceForce = 20f;
	[SerializeField] private float _cooldown = 40f;
	[HideInInspector] public float uses = 0;
	[HideInInspector] public float maxUses;
    #endregion

    #region properties
    private float _elapsedTime = 0f;
	private bool _canShoot = true;
    [HideInInspector] public GameObject _icebagPrefab;
    #endregion
    #region references
    private Transform _shotPoint;
    #endregion

    private void Start()
	{
		_shotPoint = PlayerManager.Instance.attackPoint.transform;
		_elapsedTime = 0;
		LanzaHielo();
	}

    private void Update()
    {
		_elapsedTime += Time.deltaTime;
		if (uses < maxUses && _elapsedTime >= _cooldown) _canShoot = true;
		if (Input.GetKeyDown(KeyCode.Q) && _canShoot) LanzaHielo(); 
	}

	void LanzaHielo()
	{
		_canShoot = false; 
		_elapsedTime = 0;
		_shotPoint = PlayerManager.Instance.attackPoint.transform;
		GameObject nuevoIcebagPrefab = Instantiate(_icebagPrefab, _shotPoint.position, Quaternion.identity);
		Debug.Log(nuevoIcebagPrefab.transform.position);
		Rigidbody2D rb = nuevoIcebagPrefab.GetComponent<Rigidbody2D>();
		rb.rotation = PlayerManager.Instance.gun.GetComponent<Rigidbody2D>().rotation;
		rb.AddForce(_shotPoint.right * _iceForce, ForceMode2D.Impulse);
	}
}
