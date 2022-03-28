using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeMovement : MonoBehaviour
{
    #region properties
    private float _elapsedTime = 0f;
    private Vector3[] _playerPositions;
    private enum followStates { Player, Positions};
    private followStates _myFollowStates;
    private int _wallsLayerMask;
    private int _arrayIndex;
    private int _arrayFollowIndex;
    Vector3 direction;
    Vector3 directionNormalized;
    private int signDirectionPositions;
    #endregion
    #region parameters
    [SerializeField]
    private int _numberOfPositions;
    [SerializeField]
    private float _speed = 1f, _cooldownTime = 0.1f, _distanceOffset = 1f;
    #endregion
    #region methods
    private void InicializaPosiciones(Vector3[] v)
    {
        for(int i = 0; i < v.Length; i++)
        {
            v[i] = _myPlayerManager._playerPosition;
        }
    }
    private void ChangeState()
    {
        direction = _myPlayerManager._playerPosition - _myTransform.position;
        float distance = direction.magnitude;
        directionNormalized = direction.normalized;
        if (!Physics2D.Raycast(_myTransform.position, directionNormalized, distance, _wallsLayerMask))
        {
            _myFollowStates = followStates.Player;
        }
        else
        {
            _myFollowStates = followStates.Positions;
        }
    }

    private int Sign(Vector3 v, Vector3 w)
    {
        int a = 1;
        Vector3 z = v - w;
        if (z.x + z.y < 0) { a = -1; }
        return a;
    }
    #endregion
    #region references
    private PlayerManager _myPlayerManager;
    private Transform _myTransform;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myPlayerManager = PlayerManager.Instance;
        _playerPositions = new Vector3[_numberOfPositions];
        _wallsLayerMask = 1 << 8;
        _myFollowStates = followStates.Player;
        _myTransform = transform;
       //InicializaPosiciones(_playerPositions);
        _arrayIndex = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        ChangeState();

        if(_myFollowStates == followStates.Player)
        {
            
            if((_myTransform.position - _myPlayerManager._playerPosition).magnitude > _distanceOffset)
            {
                _myTransform.position += directionNormalized * _speed * Time.deltaTime;
            }
            if (_elapsedTime > _cooldownTime)
            {
                _playerPositions[_arrayIndex % _numberOfPositions] = _myPlayerManager._playerPosition;
                _arrayIndex++;
                _elapsedTime = 0f;
            }
            _elapsedTime += Time.deltaTime;

            _arrayFollowIndex = _arrayIndex + 1;
            signDirectionPositions = Sign(_myTransform.position, _playerPositions[_arrayFollowIndex % _numberOfPositions]);
        }
        else
        {
            directionNormalized = (_playerPositions[_arrayFollowIndex % _numberOfPositions] - _myTransform.position).normalized;
            if (signDirectionPositions != Sign(_myTransform.position, _playerPositions[_arrayFollowIndex % _numberOfPositions]))
            {
                _arrayFollowIndex++;
                signDirectionPositions = Sign(_myTransform.position, _playerPositions[_arrayFollowIndex % _numberOfPositions]);
            }
            else
            {
                directionNormalized = (_playerPositions[_arrayFollowIndex % _numberOfPositions] - _myTransform.position).normalized;
                _myTransform.position += directionNormalized * _speed * Time.deltaTime;
            }
            
        }

    }
}
