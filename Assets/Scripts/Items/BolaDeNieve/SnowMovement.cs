using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMovement : MonoBehaviour
{
    #region properties
    [HideInInspector]
    public Vector3 movement;
    #endregion
    #region parameters
    [SerializeField]
    private float _velocity = 1f;
    #endregion
    #region references
    private Rigidbody2D _rb;
    #endregion
    #region methods
    public void SetMovement(Vector3 direction)
    {
        movement = direction;
        _rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
    #endregion
    
    void Start()
    {
        movement = Vector3.zero;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(movement != Vector3.zero)
        {
            _rb.velocity = movement * _velocity;
        }
    }
}
