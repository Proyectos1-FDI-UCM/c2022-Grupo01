using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeSpin : MonoBehaviour
{
    #region properties
    private Vector3 _rotation;
    #endregion
    #region references
    [SerializeField]
    private float _spinVelocity = 1f;
    #endregion
    #region references
    private Transform _myTransform;
    #endregion
    private void Start()
    {
        _myTransform = transform;
        _rotation = new Vector3(0, 0, _spinVelocity);
    }
    void Update()
    {
        transform.Rotate(_rotation);
    }
}
