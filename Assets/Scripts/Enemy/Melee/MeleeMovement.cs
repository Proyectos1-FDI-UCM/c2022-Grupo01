using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeMovement : MonoBehaviour
{
    #region properties
    private NavMeshAgent _agent;
    private float _elapsedTime = 0;
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
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > _timeToBeElapsed)
		{
            _agent.SetDestination(_myPlayerManager._playerPosition);
            _elapsedTime = 0;
		}
    }
}

