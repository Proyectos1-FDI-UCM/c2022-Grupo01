using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectPlayer : MonoBehaviour
{

    #region properties
    private float _radius;
    private enum typeofEnemy { CAC, Range, Fleeing, Necromancer }
    private int _wallsLayerMask;
    #endregion
    #region parameters
    [SerializeField]
    private typeofEnemy _thisTypeOfEnemy;
    #endregion
    #region methods
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerAttack _myPlayerAttack = other.gameObject.GetComponent<PlayerAttack>();
        if (_myPlayerAttack != null)
        {
            Vector2 direction = (other.gameObject.transform.position - _myTransform.position).normalized;
            if (!Physics2D.Raycast(_myTransform.position, direction, _radius, _wallsLayerMask))
            {
                switch (_thisTypeOfEnemy)
                {
                    case typeofEnemy.Fleeing: { GetComponentInParent<FleeingEnemyMovement>().ExecuteFleeingEnemyMovement(); break; }
                    case typeofEnemy.CAC: { GetComponentInParent<NavMeshAgent>().enabled = true; GetComponentInParent<MeleeMovement>().ExecuteMeleeEnemyMovement();  GetComponentInParent<MeleeAttack>().ExecuteMeleeAttack(); break; }
                    case typeofEnemy.Range: { GetComponentInParent<RangeMovement>().ExecuteRangeEnemyMovement(); GetComponentInParent<RangeAttack>().ExecuteRangeAttack(); break; }
                    case typeofEnemy.Necromancer: { GetComponentInParent<NecromancerController>().ExecuteNecromancerController(); break; }
                }
                this.gameObject.SetActive(false);
            }
        }
    }
    #endregion
    #region references
    private Transform _myTransform;
    private CircleCollider2D _myCircleCollider2D;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _myCircleCollider2D = GetComponent<CircleCollider2D>();
        _radius = _myCircleCollider2D.radius;
        _wallsLayerMask = 1 << 8;
    }
}
