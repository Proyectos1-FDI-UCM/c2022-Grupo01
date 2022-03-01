using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaDeHielo : MonoBehaviour
{
    private float lifeTime = 3f;
    public float iceDamage = 20f;
    private Transform _myTransform;
    private Rigidbody2D icebagRB;
    [SerializeField] private float _iceCooldown = 3f;
    private float _iceCooldownCounter = 0f;

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D punto = collision.GetContact(0);
		Destroy(gameObject);
    }
    #endregion

    void Update()
    {
        _iceCooldownCounter += Time.deltaTime;

        if (_iceCooldownCounter >= _iceCooldown)
        {
            Destroy(gameObject);
        }
    }
}