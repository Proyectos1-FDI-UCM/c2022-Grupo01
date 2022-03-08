using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FregonaController : MonoBehaviour
{
    #region parameters
    [SerializeField] private float timeToComplete = 0.5f;
    [HideInInspector] public int _uses = 0;
    [HideInInspector] public FregonaActivo fregonaActivo;
    #endregion

    #region properties
    private float _elapsedTime;
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
        Debug.Log(_uses);
        _myTransform.position = PlayerManager.Instance._playerPosition;
        if (Input.GetKey(KeyCode.Q) && _uses != 0 && fregar)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= timeToComplete) Use(); 
        }
    }

    void Use()
    {
        PlayerManager.Instance.ChangePlayerLife(2 * (int)_newPuddle._touchHydrate);
        _newPuddle.UsedPuddle();
        _elapsedTime = 0;
        _uses--;
        GameManager.Instance.SetUsesText(_uses);
    }
}