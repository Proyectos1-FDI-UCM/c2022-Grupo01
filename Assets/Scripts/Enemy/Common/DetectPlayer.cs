using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectPlayer : MonoBehaviour
{

    #region properties
    /// <summary>
    /// Radio de detección del jugador por parte de los enemigos
    /// </summary>
    private float _radius;

    /// <summary>
    /// Sala en la que se encuentra el jugador
    /// </summary>
    [HideInInspector] public SalaManager sala;

    /// <summary>
    /// Estados de los enemigos
    /// </summary>
    private enum DetectStates { Inactive, Stand, Detected};
    private DetectStates _myState;

    /// <summary>
    /// Tipo de enemigo
    /// </summary>
    private enum typeofEnemy { CAC, Range, Fleeing, Necromancer };

    /// <summary>
    /// LayerMask de las paredes
    /// </summary>
    private int _wallsLayerMask;
    #endregion

    #region parameters
    [SerializeField] private typeofEnemy _thisTypeOfEnemy;
    #endregion


    #region methods
    private void OnTriggerStay2D(Collider2D other)
    {
        if(_myState == DetectStates.Stand)
        {
            PlayerAttack _myPlayerAttack = other.gameObject.GetComponent<PlayerAttack>();
            if (_myPlayerAttack != null)
            {
                Vector2 direction = (other.gameObject.transform.position - _myTransform.position).normalized;
                if (!Physics2D.Raycast(_myTransform.position, direction, _radius, _wallsLayerMask))
                {
                    switch (_thisTypeOfEnemy)
                    {
                        case typeofEnemy.Fleeing: GetComponentInParent<FleeingEnemyMovement>().ExecuteFleeingEnemyMovement(); break;
                        case typeofEnemy.CAC:
                            GetComponentInParent<NavMeshAgent>().enabled = true;
                            GetComponentInParent<MeleeMovement>().ExecuteMeleeEnemyMovement();
                            GetComponentInParent<MeleeAttack>().ExecuteMeleeAttack();
                            break;
                        case typeofEnemy.Range:
                            GetComponentInParent<RangeMovement>().ExecuteRangeEnemyMovement();
                            GetComponentInParent<RangeAttack>().ExecuteRangeAttack();
                            break;
                        case typeofEnemy.Necromancer: GetComponentInParent<NecromancerController>().ExecuteNecromancerController(); break;
                    }
                    _myState = DetectStates.Detected;
                }
            }
        }
        else if(sala.myState == SalaManager.SalaStates.Inactiva)
        {
            switch (_thisTypeOfEnemy)
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
            }
            _myState = DetectStates.Stand;
        }
    }
    #endregion

    #region references
    /// <summary>
    /// Referencia al transform del enemigo
    /// </summary>
    private Transform _myTransform;

    /// <summary>
    /// Trigger del radio
    /// </summary>
    private CircleCollider2D _myCircleCollider2D;
    #endregion

    void Start()
    {
        _myTransform = transform;
        _myCircleCollider2D = GetComponent<CircleCollider2D>();
        _radius = _myCircleCollider2D.radius;
        _wallsLayerMask = 1 << 8;
        _myState = DetectStates.Inactive;
    }
}
