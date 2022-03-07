using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fregona : MonoBehaviour
{
    #region parameters
    [SerializeField] private float timeToComplete = 0.5f;
    [HideInInspector] public int maxUses;
    #endregion
    #region properties
    private float _elapsedTime;
    private int _uses = 0;
    private Puddle _puddle;
    private Puddle _newPuddle;
    private bool fregar = false;
    #endregion
    #region references
    private Transform _myTransform;
    #endregion

    private void Start()
    {
        _myTransform = transform;
        _uses = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _puddle = other.gameObject.GetComponent<Puddle>();
        if (_puddle != null)
        {
            _newPuddle = _puddle;
            fregar = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _puddle = collision.gameObject.GetComponent<Puddle>();
        if (_puddle != null)
        {
            _newPuddle = _puddle;
            fregar = false;
        }
    }

    private void Update()
    {
        _myTransform.position = PlayerManager.Instance._playerPosition;
        if (_uses < maxUses && fregar)
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
        PlayerManager.Instance.ChangePlayerLife(2 * (int)_newPuddle._touchHydrate);
        _newPuddle.UsedPuddle();
        _elapsedTime = 0;
        _uses++;
    }
}