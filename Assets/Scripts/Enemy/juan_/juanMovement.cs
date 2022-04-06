using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juanMovement : MonoBehaviour
{
    #region properties
    private enum juanStates {Channel, Move, Teleport, Rest };
    private juanStates _myState;
    private float _elapsedTime;
    private Vector3 _playerDirection, _lastDirecion;
    private Vector3 _playerPosition;
    private bool _collision, rest;
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
    private float _cooldownPortalTime = 0.75f;
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
    private Collider2D _collider;
    private Transform _myTransform;
    private SpriteRenderer _mySpriteRenderer;
    private EnemyLifeComponent _myELC;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject _trapDoor;
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
        _collider = GetComponent<Collider2D>();
        _collision = false;
    }
    private void OnEnable()
    {
        EnemyLifeComponent _myJuanLifeComponent = GetComponent<EnemyLifeComponent>();
        GameManager.Instance.CreateBossBar("Juan.", _myJuanLifeComponent.maxLife);
        _myState = juanStates.Channel;
        bossTeleport();
        spawn();
        _collider.isTrigger = false;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myPlayerManager = PlayerManager.Instance;
        _myELC = GetComponent<EnemyLifeComponent>();
        //_wallsLayerMask = 1 << 8;
    }
        
    // Update is called once per frame
    void Update()
    {

        if (_myState == juanStates.Channel)
        {
            animator.SetBool("CHARGE",true);
            _playerPosition = _myPlayerManager._playerPosition;
            _playerDirection = (_playerPosition - _myTransform.position).normalized; 
            if (_playerDirection.x > 0) _mySpriteRenderer.flipX = true;
            else _mySpriteRenderer.flipX = false;

            if (cooldownsTime(_cooldownChannelTime))
            {

                animator.SetBool("CHARGE", false);
                _myState = juanStates.Move;
                _rb.constraints = RigidbodyConstraints2D.None;
                _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

                
                _elapsedTime = 0;
            }
        }
        else if(_myState == juanStates.Move)
        {
            
            if (!_collision)
            {
                _rb.velocity = _playerDirection * _speed;
                animator.SetBool("MOVE", true);

            }

            if ((_playerPosition + _movementOffset * _playerDirection - _myTransform.position).magnitude < _distanceOffset || _collision)
            {
                animator.SetBool("MOVE", false);
                _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                _rb.velocity = Vector2.zero;
                _myState = juanStates.Rest;
                _collision = false;
                rest = true;

            }
        }
        else if(_myState == juanStates.Rest)
        {
            
            if (rest&&cooldownsTime(_cooldownRestTime))
            {
                _collider.isTrigger = true;
                animator.SetBool("PORTAL", true);
                

                _elapsedTime = 0;
                rest = false;
                
            }
            else if (!rest&&cooldownsTime(_cooldownPortalTime))
            {
                
                animator.SetBool("PORTAL", false);
                animator.SetBool("HURT", false);


                _myState = juanStates.Teleport;
                bossTeleport();
                _elapsedTime = 0;
            }
        }
        else
        {
            if (cooldownsTime(_cooldownSpawnTime))
            {
                _collider.isTrigger = false;
                _myState = juanStates.Channel;
                spawn();
                _elapsedTime = 0;
                
            }
        }


        
    }
    private void OnDestroy()
    {
        _trapDoor.gameObject.SetActive(true);
    }
}
