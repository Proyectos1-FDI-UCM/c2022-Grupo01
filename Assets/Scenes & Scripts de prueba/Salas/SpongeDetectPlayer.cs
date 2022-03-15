using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeDetectPlayer : MonoBehaviour
{
    #region properties
    /// <summary>
    /// Radio de detección de los enemigos
    /// </summary>
    private float _radius;

    /// <summary>
    /// Sala en la que se encuentra el enemigo
    /// </summary>
    [HideInInspector] public SpongeSalaManager sala;

    /// <summary>
    /// Estados en los que puede encontrarse el enemigo
    /// </summary>
    public enum DetectStates { Stand, Detected };

    /// <summary>
    /// Tipos de enemigo
    /// </summary>
    private enum typeofEnemy { Range };

    /// <summary>
    /// Número con LayerMask de la pared
    /// </summary>
    private int _wallsLayerMask;
    #endregion

    #region parameters
    public DetectStates _myState;

    [SerializeField] private typeofEnemy _thisTypeOfEnemy;
    #endregion

    #region references
    /// <summary>
    /// Referencia al transform del enemigo
    /// </summary>
    private Transform _myTransform;

    /// <summary>
    /// Referencia al collider de los enemigos
    /// </summary>
    private CircleCollider2D _myCircleCollider2D;

    /// <summary>
    /// Referencia al rigidbody del enemigo
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// Referencia al EnemyLifeComponent del enemigo
    /// </summary>
    private SpongeLifeComponent _myELC;
    #endregion


    #region methods
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_myState == DetectStates.Stand)
        {
            PlayerAttack _myPlayerAttack = other.gameObject.GetComponent<PlayerAttack>();

            if (_myPlayerAttack != null)
            {
                Vector2 direction = (other.gameObject.transform.position - _myTransform.position).normalized;

                if (!Physics2D.Raycast(_myTransform.position, direction, _radius, _wallsLayerMask))
                {
                    //Debug.Log("Activate");
                    Activate();
                }
            }
        }

    }

    /// <summary>
    /// Activa los distintos enemigos (movimiento, disparo, etc...) una vez detectados
    /// </summary>
    public void Activate()
    {
        _myState = DetectStates.Detected;
        GetComponentInParent<RangeAttack>().ExecuteRangeAttack();
        /*switch (_thisTypeOfEnemy)
        {
            case typeofEnemy.Fleeing: GetComponentInParent<FleeingEnemyMovement>().ExecuteFleeingEnemyMovement(); break;
            case typeofEnemy.CAC:
                GetComponentInParent<NavMeshAgent>().enabled = true;
                GetComponentInParent<MeleeMovement>().ExecuteMeleeEnemyMovement();
                GetComponentInParent<MeleeAttack>().ExecuteMeleeAttack();
                break;
            case typeofEnemy.Range:
                //GetComponentInParent<RangeMovement>().ExecuteRangeEnemyMovement();
                GetComponentInParent<RangeAttack>().ExecuteRangeAttack();
                break;
            case typeofEnemy.Necromancer:
                GetComponentInParent<NecromancerController>().ExecuteNecromancerController();
                //Debug.Log("Si");
                break;
            case typeofEnemy.Weak:
                GetComponentInParent<NavMeshAgent>().enabled = true;
                GetComponentInParent<MeleeMovement>().ExecuteMeleeEnemyMovement();
                break; 
        }*/
    }

    /// <summary>
    /// Desactiva los distintos enemigos si el jugador ha salido del rango de detección
    /// </summary>
    public void Deactivate()
    {
        _myState = DetectStates.Stand;
        GetComponentInParent<RangeAttack>().enabled = false;
        /*switch (_thisTypeOfEnemy)
        {
            case typeofEnemy.Fleeing: GetComponentInParent<FleeingEnemyMovement>().enabled = false; break;
            case typeofEnemy.CAC:
                GetComponentInParent<NavMeshAgent>().enabled = false;
                GetComponentInParent<MeleeMovement>().StopMeleeEnemyMovement();
                GetComponentInParent<MeleeAttack>().enabled = false;
                break; 
            case typeofEnemy.Range:
                GetComponentInParent<RangeMovement>().enabled = false;
                GetComponentInParent<RangeAttack>().enabled = false;
                break;
            case typeofEnemy.Necromancer: GetComponentInParent<NecromancerController>().enabled = false; break;
            case typeofEnemy.Weak:
                GetComponentInParent<NavMeshAgent>().enabled = false;
                GetComponentInParent<MeleeMovement>().StopMeleeEnemyMovement();
                break; 
        }*/
    }
    #endregion

    void Start()
    {
        _myTransform = transform;
        _myCircleCollider2D = GetComponent<CircleCollider2D>();
        _radius = _myCircleCollider2D.radius;
        _wallsLayerMask = 1 << 8;
        _myState = DetectStates.Stand;
        _rb = GetComponentInParent<Rigidbody2D>();
        _myELC = GetComponentInParent<SpongeLifeComponent>();
    }

    void Update()
    {
        if (_myELC.dead)
        {
            Deactivate();
        }
    }
}
