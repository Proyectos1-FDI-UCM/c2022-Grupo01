using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FregonaController : MonoBehaviour
{
    #region parameters
    [SerializeField] private float timeToComplete = 0.5f;
    [HideInInspector] public FregonaActivo fregonaActivo;
    [HideInInspector] public float _cooldown;
    #endregion

    #region properties
    [HideInInspector] public float _elapsedTime;
    private float _timeUntilComplete;
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
        _elapsedTime += Time.deltaTime;
        GameManager.Instance.ShowActiveCooldown(_elapsedTime, _cooldown);
        _myTransform.position = PlayerManager.Instance._playerPosition;
        if (_elapsedTime >= _cooldown && Input.GetKey(KeyCode.Q) && fregar)
        {
            _timeUntilComplete += Time.deltaTime;
            if (_timeUntilComplete >= timeToComplete) Use();
        }
    }

    void Use()
    {
        PlayerManager.Instance.ChangePlayerLife(2 * (int)_newPuddle._touchHydrate);
        _newPuddle.UsedPuddle();
        _timeUntilComplete = 0;
        _elapsedTime = 0;
    }
}