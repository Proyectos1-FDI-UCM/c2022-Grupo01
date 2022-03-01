using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fregona : MonoBehaviour
{
    #region parameters
    [SerializeField] private float timeToComplete = 0.5f;
    [SerializeField] private int maxUses = 3;
    #endregion
    #region properties
    private float _elapsedTime;
    private int _uses = 0;
    private Puddle _puddle;
    private bool fregar = false;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        _puddle = other.gameObject.GetComponent<Puddle>();
        if (_puddle != null)
        {
            fregar = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _puddle = collision.gameObject.GetComponent<Puddle>();
        if (_puddle != null)
        {
            fregar = false;
        }
    }

    private void Update()
    {
        if (fregar)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= timeToComplete)
                {
                    Use();
                }
            }
        }
    }

    void Use()
    {
        PlayerManager.Instance.ChangePlayerLife(2 * (int)_puddle._touchHydrate);
        _puddle.UsedPuddle();
        _elapsedTime = 0;
        _uses++;
        if (_uses == maxUses)
        {
            enabled = false;
        }
    }
}