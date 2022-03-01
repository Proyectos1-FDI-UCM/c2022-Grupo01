using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    #region references
    private Transform _myTransform;
    private PlayerManager _myPlayerManager;
    private BubbleTrigger _myBubbleTrigger;
    private Rigidbody2D _myRB;
    private Transform _enemyTransform;
    #endregion
    #region properties
    public enum BubbleStates { Stand, Detected, Follow};
    public BubbleStates _myState = BubbleStates.Stand;
    private int _sign;
    #endregion
    #region parameters
    [SerializeField]
    private Vector3 _positionOffset = new Vector3(-0.3f, 0.78f, 0);
    [SerializeField]
    private float _lerpOffset, _shotForce;
    #endregion
    #region methods
 
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myPlayerManager = PlayerManager.Instance;
        _myBubbleTrigger = GetComponentInChildren<BubbleTrigger>();
        _myRB = GetComponent<Rigidbody2D>();
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = _myPlayerManager._playerPosition + _positionOffset;
        Vector3 direction = (desiredPosition - _myTransform.position);
        float distance = direction.magnitude;
        if (_myState == BubbleStates.Stand)
        {
            if (distance > 0.5)
            {
                _myRB.velocity = direction * _shotForce * Time.deltaTime * 1000;
            }
            else
            {
                _myRB.velocity = Vector3.zero;
            }
        }
        else if(_myState == BubbleStates.Detected)
        {
            _enemyTransform = _myBubbleTrigger.GetEnemyTransform();
            _myState = BubbleStates.Follow;
        }
        else
        {
            Vector3 direction2 = (_enemyTransform.position - _myTransform.position).normalized;
            _myRB.velocity = direction2 * _shotForce * Time.deltaTime * 1000;
        }

    }
    
}
