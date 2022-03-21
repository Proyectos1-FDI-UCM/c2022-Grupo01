using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juanMovement : MonoBehaviour
{
    #region properties
    private enum juanStates { Channel, Move, Teleport, Rest };
    private juanStates _myState;
    private float _elapsedTime;
    private Vector3 _playerDirection;
    private Vector3 _playerPosition;
    private bool _collision;
    [SerializeField]
    private Transform[] _teleportPoints = new Transform [5];
    //private int _wallsLayerMask;
    #endregion

    #region parameters
    [SerializeField]
    private float _cooldownChannelTime = 2f;
    [SerializeField]
    private float _cooldownRestTime = 2f;
    [SerializeField]
    private float _cooldownSpawnTime = 2f;
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _distanceOffset = 0.1f;
    [SerializeField]
    private float _movementOffset = 2f;
    #endregion

    #region references
    private PlayerManager _myPlayerManager;
    private Rigidbody2D _rb;
    private Transform _myTransform;
    private SpriteRenderer _mySpriteRenderer;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerAttack _myPA = collision.gameObject.GetComponent<PlayerAttack>();
        if(_myPA == null) // Solo si no es el jugador
        {
            _rb.velocity = Vector2.zero;
            _collision = true;
        }
        
    }

    private bool cooldownsTime(float cooldownTime)
    {
        _elapsedTime += Time.deltaTime;
        return _elapsedTime > cooldownTime; 
    }

    private void bossTeleport()
    {
        _mySpriteRenderer.enabled = false;
        _rb.constraints = RigidbodyConstraints2D.None;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _myTransform.position = _teleportPoints[Random.Range(0, 5)].position;
    }

    private void spawn()
    {
        _mySpriteRenderer.enabled = true;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myState = juanStates.Channel;
        _myPlayerManager = PlayerManager.Instance;
        _elapsedTime = 0;
        _rb = GetComponent<Rigidbody2D>();
        _myTransform = transform;
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _myTransform.position = _teleportPoints[0].position;
        _collision = false;
        //_wallsLayerMask = 1 << 8;
    }

    // Update is called once per frame
    void Update()
    {
        if(_myState == juanStates.Channel)
        {
            Debug.Log("Canalizar");
            if(cooldownsTime(_cooldownChannelTime))
            {
                _playerPosition = _myPlayerManager._playerPosition;
                _playerDirection = (_playerPosition - _myTransform.position).normalized;
                _myState = juanStates.Move;
                _elapsedTime = 0;
            }
        }
        else if(_myState == juanStates.Move)
        {
            if (!_collision)
            {
                _rb.velocity = _playerDirection * _speed;
            }


            if ((_playerPosition + _movementOffset * _playerDirection - _myTransform.position).magnitude < _distanceOffset || _rb.velocity == Vector2.zero)
            {
                Debug.Log("Rest");
                _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                _rb.velocity = Vector2.zero;
                _myState = juanStates.Rest;
                _collision = false;
                
            }
        }
        else if(_myState == juanStates.Rest)
        {
            if (cooldownsTime(_cooldownRestTime))
            {
                Debug.Log("TP");

                _myState = juanStates.Teleport;
                _elapsedTime = 0;
                bossTeleport();
            }
        }
        else
        {
            Debug.Log("Spawn");

            if (cooldownsTime(_cooldownSpawnTime))
            {
                _myState = juanStates.Channel;
                _elapsedTime = 0;
                spawn();
            }
        }

    }
}
