using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaDeHielo : MonoBehaviour
{
    #region parameters
    [SerializeField] private float lifeTime = 0.8f;
    [SerializeField] private float _iceCooldown = 3f;
    #endregion

    #region properties
    private float _iceCooldownCounter = 0f;
    #endregion

    #region references
    [SerializeField] private GameObject _areaEffect;
    #endregion

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
    }

    void Explode()
    {
        Destroy(gameObject);
        Instantiate(_areaEffect, transform.position, Quaternion.identity);
    }
    #endregion

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        _iceCooldownCounter += Time.deltaTime;

        if (_iceCooldownCounter >= _iceCooldown)
        {
            Explode();
        }
    }

	private void OnDestroy()
	{
        Explode();
	}

}