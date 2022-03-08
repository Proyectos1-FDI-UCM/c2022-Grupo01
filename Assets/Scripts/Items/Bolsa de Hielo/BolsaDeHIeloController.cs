using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaDeHieloController : MonoBehaviour
{
    #region parameters
	[HideInInspector] public float _iceForce = 20f;
	[HideInInspector] public float _cooldown = 40f;
    [HideInInspector] public float _elapsedTime = 0f;
	[HideInInspector] public int maxUses;
    #endregion

    #region properties
    [HideInInspector] public GameObject _icebagPrefab;
	private int uses;
    #endregion

    #region references
    private Transform _shotPoint;
    #endregion

    private void Start()
	{
		_shotPoint = PlayerManager.Instance.attackPoint.transform;
		LanzaHielo();
	}

	private void Update()
    {
		_elapsedTime += Time.deltaTime;
		GameManager.Instance.ShowActiveCooldown(_elapsedTime, _cooldown);
		if (Input.GetKeyDown(KeyCode.Q) && uses < maxUses &&_elapsedTime >= _cooldown) LanzaHielo();
	}

	public void LanzaHielo()
	{
		_elapsedTime = 0;
		_shotPoint = PlayerManager.Instance.attackPoint.transform;
		GameObject nuevoIcebagPrefab = Instantiate(_icebagPrefab, _shotPoint.position, Quaternion.identity);
		Rigidbody2D rb = nuevoIcebagPrefab.GetComponent<Rigidbody2D>();
		rb.rotation = PlayerManager.Instance.gun.GetComponent<Rigidbody2D>().rotation;
		rb.AddForce(_shotPoint.right * _iceForce, ForceMode2D.Impulse);
		uses++;
		GameManager.Instance.SetUsesText(maxUses - uses);
	}
}