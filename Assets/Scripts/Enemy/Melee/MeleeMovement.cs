using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeMovement : MonoBehaviour
{
    #region properties
    private NavMeshAgent _agent;
    private float _elapsedTime = 0;
    [SerializeField]
    private float _playerDistance = 1;
    #endregion
    #region parameters
    [SerializeField]
    private float _timeToBeElapsed = 2f;
    #endregion
    #region methods
    public void ExecuteMeleeEnemyMovement()
    {
        this.enabled = true;
        _animator.SetBool("Walk", true);   
    }
    public void StopMeleeEnemyMovement()
    {
        this.enabled = false;
        _animator.SetBool("Walk", false);
    }
    #endregion
    #region references
    [SerializeField]
    private Animator _animator;
    private PlayerManager _myPlayerManager;
    private Transform _mytransform;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myPlayerManager = PlayerManager.Instance;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _elapsedTime += _timeToBeElapsed;
        _agent.SetDestination(_myPlayerManager._playerPosition);
        _mytransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;

        Vector3 direction = ( _myPlayerManager._playerPosition-_mytransform.position).normalized;
       // float distance = direction.magnitude;

        if( _elapsedTime > _timeToBeElapsed)
		{
            _agent.SetDestination(_myPlayerManager._playerPosition-direction*_playerDistance);
            
            _elapsedTime = 0;
		}
    }
}

