using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingEnemyMovement : MonoBehaviour
{
    #region parameters
    public float _speed;
    private Vector3 _enemyMovementDirection;
    [SerializeField] private float _hidingTimer = 3.0f;
    #endregion

    #region references
    /// <summary>
    /// Reference to fleeing enemy rigidbody
    /// </summary>
    public Rigidbody2D fleeingEnemyRigidbody;
    public Transform fleeingEnemyTransform;
    private PlayerManager _myPlayerManager;
    public Animator fleeingEnemyAnimator;
    #endregion

    #region properties
    public bool onCollision;
    private float _timer;
    #endregion

    #region methods
    public void ExecuteFleeingEnemyMovement()
    {
        this.enabled = true;
    }

    public void FleeingEnemyHiding()
    {
        Destroy(gameObject, 1f); 
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myPlayerManager = PlayerManager.Instance;
        fleeingEnemyRigidbody = GetComponent<Rigidbody2D>();
        fleeingEnemyTransform = transform;
        fleeingEnemyAnimator = GetComponent<Animator>();
        _speed = 4.5f;
        _timer = _hidingTimer;
    }

    void Update()
    {

            if (_timer <= 0)
            {
                FleeingEnemyHiding();
            }
 
    }

    private void FixedUpdate()
    {
        // es un test -> Quaternion rotacion = Quaternion.Euler(Time.time % 10 * 100, Time.time % 10 * 100, 0);

        _enemyMovementDirection = (fleeingEnemyTransform.position - _myPlayerManager._playerPosition).normalized;
        fleeingEnemyRigidbody.MovePosition(fleeingEnemyTransform.position + _enemyMovementDirection * _speed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8) _timer -= Time.deltaTime;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _timer = _hidingTimer;
    }
}
