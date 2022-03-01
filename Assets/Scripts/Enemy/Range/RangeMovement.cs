using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMovement : MonoBehaviour
{
    #region properties
    private enum RangeStates {Flee, Follow, Stand};
    private RangeStates _myRangeStates;
    private float _elapsedTime;
    private int _wallsLayerMask;
    #endregion
    #region parameters
    [SerializeField]
    private float _wallDistanceOffset;
    [SerializeField]
    private float _speed, _distanceFlee, _raycastTime;
    #endregion
    #region methods
    public void ExecuteRangeEnemyMovement()
    {
        this.enabled = true;
    }
    #endregion
    #region references
    private Transform _myTransform;
    private PlayerManager _myPlayerManager;
    private Rigidbody2D _rb;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myPlayerManager = PlayerManager.Instance;
        _myTransform = transform;
        _rb = GetComponent<Rigidbody2D>();
        _elapsedTime = _raycastTime + 1;
        _myRangeStates = RangeStates.Stand;
        _wallsLayerMask = 1 << 8;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (_myTransform.position - _myPlayerManager._playerPosition).normalized;
        float distance = (_myTransform.position - _myPlayerManager._playerPosition).magnitude;
        if (_elapsedTime > _raycastTime)
        {
            
            if (distance < _distanceFlee)
            {
                if (!Physics2D.Raycast(_myTransform.position, direction, _wallDistanceOffset, _wallsLayerMask) && _myRangeStates != RangeStates.Follow)
                {
                    _myRangeStates = RangeStates.Flee;
                }
                else
                {
                    _myRangeStates = RangeStates.Follow;
                }
            }
            else
            {
                _myRangeStates = RangeStates.Stand;
            }
            _elapsedTime = 0;
        }

        if (_myRangeStates == RangeStates.Flee)
        {
            _rb.MovePosition(_myTransform.position + direction * _speed * Time.deltaTime);
        }
        else if (_myRangeStates == RangeStates.Follow)
        {
            _rb.MovePosition(_myTransform.position - direction * _speed * Time.deltaTime);
        }
        _elapsedTime += Time.deltaTime;
    }
}
