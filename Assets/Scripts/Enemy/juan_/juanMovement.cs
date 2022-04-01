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
    private Transform[] _teleportPoints = new Transform [6];
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
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer _trapDoor;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        BulletLife bullet = collision.gameObject.GetComponent<BulletLife>();


        if (bullet == null && _myState == juanStates.Move)
        {
            _rb.velocity = Vector2.zero;
            _collision = true;
        }
        else if (bullet != null)
        {
            _rb.velocity = Vector2.zero;
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
        //_rb.constraints = RigidbodyConstraints2D.None;
        //_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _myTransform.position = _teleportPoints[Random.Range(0, 6)].position;
    }

    private void spawn()
    {
        _mySpriteRenderer.enabled = true;
    }

    private void Awake()
    {
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _myState = juanStates.Channel;
        _elapsedTime = 0;
        _rb = GetComponent<Rigidbody2D>();
        _myTransform = transform;
        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _collision = false;
    }
    private void OnEnable()
    {
        bossTeleport();
        spawn();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myPlayerManager = PlayerManager.Instance;
        //_wallsLayerMask = 1 << 8;
    }
        
    // Update is called once per frame
    void Update()
    {
        if(_myState == juanStates.Channel)
        {
            if(cooldownsTime(_cooldownChannelTime))
            {
                _playerPosition = _myPlayerManager._playerPosition;
                _playerDirection = (_playerPosition - _myTransform.position).normalized;
                _myState = juanStates.Move;
                _rb.constraints = RigidbodyConstraints2D.None;
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

                _elapsedTime = 0;
            }
        }
        else if(_myState == juanStates.Move)
        {
            animator.SetTrigger("MOVE");
            if (!_collision)
            {
                _rb.velocity = _playerDirection * _speed;
            }

            if ((_playerPosition + _movementOffset * _playerDirection - _myTransform.position).magnitude < _distanceOffset || _collision)
            {
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

                _myState = juanStates.Teleport;
                _elapsedTime = 0;
                bossTeleport();
            }
        }
        else
        {
            if (cooldownsTime(_cooldownSpawnTime))
            {
                _myState = juanStates.Channel;
                _elapsedTime = 0;
                spawn();
            }
        }
    }

    private void OnDestroy()
    {
        _trapDoor.enabled = true;
        _trapDoor.GetComponent<NextLevel>().enabled = true;
    }
}
