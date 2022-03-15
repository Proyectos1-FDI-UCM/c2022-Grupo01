using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingEnemyMovement : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Velocidad del huidizo
    /// </summary>
    public float _speed;

    /// <summary>
    /// Vector dirección de movimiento del huidizo
    /// </summary>
    private Vector3 _enemyMovementDirection;

    /// <summary>
    /// Tiempo fijado de desaparición del huidizo
    /// </summary>
    [SerializeField] private float _hidingTimer = 3.0f;
    #endregion

    #region references
    /// <summary>
    /// Referencia al rigidbody del huidizo
    /// </summary>
    public Rigidbody2D fleeingEnemyRigidbody;

    /// <summary>
    /// Referencia al transform del huidizo
    /// </summary>
    public Transform fleeingEnemyTransform;

    /// <summary>
    /// Referencia al PlayerManager
    /// </summary>
    private PlayerManager _myPlayerManager;

    /// <summary>
    /// Referencia al Animator
    /// </summary>
    public Animator fleeingEnemyAnimator;

    /// <summary>
    /// Referencia al EnemyLifeComponent
    /// </summary>
    private EnemyLifeComponent _myELC;
    #endregion

    #region properties
    /// <summary>
    /// Booleano que devuelve si el huidizo está en colisión con alguna pared
    /// </summary>
    public bool onCollision;

    /// <summary>
    /// Contador de desaparición del huidizo
    /// </summary>
    private float _timer;
    #endregion

    #region methods
    /// <summary>
    /// Métod. que activa el script de movimiento del huidizo
    /// </summary>
    public void ExecuteFleeingEnemyMovement()
    {
        this.enabled = true;
    }

    /// <summary>
    /// Métod. que esconde al huidizo
    /// </summary>
    public void FleeingEnemyHiding()
    {
        _myELC.sala.OnEnemyDies(_myELC);
        Destroy(gameObject, 1f); 
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) _timer -= Time.deltaTime;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _timer = _hidingTimer;
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
        _myELC = GetComponent<EnemyLifeComponent>();
    }

    void Update()
    {
        if (_timer <= 0) FleeingEnemyHiding();
    }

    private void FixedUpdate()
    {
        // es un test -> Quaternion rotacion = Quaternion.Euler(Time.time % 10 * 100, Time.time % 10 * 100, 0);
        _enemyMovementDirection = (fleeingEnemyTransform.position - _myPlayerManager._playerPosition).normalized;
        fleeingEnemyRigidbody.MovePosition(fleeingEnemyTransform.position + _enemyMovementDirection * _speed * Time.fixedDeltaTime);
    }
}
