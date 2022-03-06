using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTrigger : MonoBehaviour
{
    #region properties
    private float _radius;
    private int _wallsLayerMask;
    public Transform _enemyTransform;
    #endregion
    #region references
    private Transform _myTransform;
    private BubbleMovement _myBubbleMovement;
    #endregion
    #region methods
    private void OnTriggerStay2D(Collider2D other)
    {
        EnemyLifeComponent _myEnemyLifeComponent = other.gameObject.GetComponent<EnemyLifeComponent>();
        if (_myEnemyLifeComponent != null)
        {
            Vector2 direction = (other.gameObject.transform.position - _myTransform.position).normalized;
            if (!Physics2D.Raycast(_myTransform.position, direction, _radius, _wallsLayerMask))
            {
                _enemyTransform = other.gameObject.GetComponent<Transform>();
                _myBubbleMovement._myState = BubbleMovement.BubbleStates.Detected;
                this.gameObject.SetActive(false);
            }
        }
    }
    public Transform GetEnemyTransform()
    {
        return _enemyTransform;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _radius = GetComponent<CircleCollider2D>().radius;
        _wallsLayerMask = 1 << 8;
        _myTransform = transform;
        _myBubbleMovement = GetComponentInParent<BubbleMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
