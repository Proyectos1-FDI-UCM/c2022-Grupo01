using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementMelee : MonoBehaviour
{
    #region properties
    private float _elapsedTime = 0f;
    private Vector3[] _playerPositions;
    private enum followStates { Player, Positions };
    private followStates _myFollowStates;
    private int _wallsLayerMask = 1 << 8;
    private int _voidLayerMask = 1 << 15;
    private int _arrayIndex;
    private int _arrayFollowIndex;
    Vector3 direction;
    Vector3 directionNormalized;
    private int signDirectionPositions;
    private bool _changeState;
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
        for (int i = 0; i < v.Length; i++)
        {
            v[i] = _myPlayerManager._playerPosition;
        }
    }

    public void ExecuteMeleeEnemyMovement()
    {
        this.enabled = true;
    }
    public void StopMeleeEnemyMovement()
    {
        this.enabled = false;
    }
    private void ChangeState()
    {
        direction = _myPlayerManager._playerPosition - _myTransform.position;
        float distance = direction.magnitude;
        directionNormalized = direction.normalized;
        if (!Physics2D.Raycast(_myTransform.position, directionNormalized, distance, _wallsLayerMask))
        {
            if (!Physics2D.Raycast(_myTransform.position, directionNormalized, distance, _voidLayerMask)) { _myFollowStates = followStates.Player; Debug.Log("i persigo"); }
        }
        else
        {
            _myFollowStates = followStates.Positions; Debug.Log("i persigon't");
        }
        Debug.DrawRay(_myTransform.position, directionNormalized, Color.blue, 0.2f, false);
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
        _myFollowStates = followStates.Player;
        _myTransform = transform;
        _arrayIndex = 0;
        _changeState = true;
        InicializaPosiciones(_playerPositions);
    }

    /*void UpdatePlayerPositions()
    {
        _playerPositions[_arrayIndex] = _myPlayerManager._playerPosition;
        _arrayIndex = (_arrayIndex + 1) % _numberOfPositions;
        _elapsedTime = 0f;
    }*/

    // Update is called once per frame
    void Update()
    {
        ChangeState();



        if (_myFollowStates == followStates.Player)
        {
            if ((_myTransform.position - _myPlayerManager._playerPosition).magnitude > _distanceOffset)
            {
                _myTransform.Translate(directionNormalized * _speed * Time.deltaTime);
            }

            _playerPositions[0] = _myPlayerManager._playerPosition;
            _changeState = true;
            //if (_elapsedTime > _cooldownTime)
            //{
            //    UpdatePlayerPositions();
            //}
            //_elapsedTime += Time.deltaTime;
        }

        else
        {
            Debug.Log("a ver");
            if (_changeState)
            {
                _arrayFollowIndex = 0;
                _arrayIndex = _arrayFollowIndex + 1;
                _changeState = false;
            }
            directionNormalized = (_playerPositions[_arrayFollowIndex] - _myTransform.position).normalized;
            if (signDirectionPositions != Sign(_myTransform.position, _playerPositions[_arrayFollowIndex]))
            {
                _playerPositions[_arrayFollowIndex] = _myPlayerManager._playerPosition;
                _arrayFollowIndex = (_arrayFollowIndex + 1) % _numberOfPositions;
                signDirectionPositions = Sign(_myTransform.position, _playerPositions[_arrayFollowIndex]);
            }
            else
            {
                directionNormalized = (_playerPositions[_arrayFollowIndex] - _myTransform.position).normalized;
                _myTransform.Translate(directionNormalized * _speed * Time.deltaTime);
            }

            if (_elapsedTime > _cooldownTime)
            {
                _playerPositions[_arrayIndex] = _myPlayerManager._playerPosition;
                _arrayIndex = (_arrayIndex + 1) % _numberOfPositions;
            }
            _elapsedTime += Time.deltaTime;
        }
    }
}