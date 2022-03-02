using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaDeHielo : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _iceCooldown = 10f;
    [SerializeField] private float _iceDamage = 20f;
    #endregion

    #region properties
    private float _iceCooldownCounter = 0f;
    [SerializeField] private float _iceRange = 60f;
    #endregion

    #region references
    [SerializeField] private GameObject _areaEffect;
    private Transform _myTransform;
    private EnemyLifeComponent _iceAttack;
    #endregion
    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
    }
    #endregion

    private void Start()
    {
        _myTransform = transform;
        _iceAttack = GetComponent<EnemyLifeComponent>();
    }

    void Update()
    {
        _iceCooldownCounter += Time.deltaTime;

        if (_iceCooldownCounter >= _iceCooldown)
        {
            Explode();
        }
    }

    void Explode()
    {
        Destroy(gameObject);
        Instantiate(_areaEffect, transform.position, Quaternion.identity);
        Collider2D[] area = Physics2D.OverlapCircleAll(_myTransform.position, _iceRange);
        foreach(Collider2D obj in area) 
        {
            if (obj.gameObject.CompareTag("Enemy"))
            {
                obj.gameObject.GetComponent<EnemyLifeComponent>().Damage(_iceDamage);
            }
        }
    }
}