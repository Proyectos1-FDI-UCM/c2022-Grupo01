using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeMovement : MonoBehaviour
{
    #region properties
    [SerializeField]
    private float _playerDistance = 1;
    private int _wallsLayerMask, _voidLayerMask;
    #endregion
    #region parameters
    [SerializeField]
    private float _speed = 1f;
    #endregion
    #region methods
    public void ExecuteMeleeEnemyMovement()
    {
        this.enabled = true;
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
        _mytransform = transform;
        _wallsLayerMask = 1 << 8;
        _voidLayerMask = 1 << 15;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = _myPlayerManager._playerPosition - _mytransform.position;
        float distance = direction.magnitude;
        Vector3 directionNormalized = direction.normalized;
        if (!Physics2D.Raycast(_mytransform.position, directionNormalized, distance, _wallsLayerMask))
        {
            if (!Physics2D.Raycast(_mytransform.position, directionNormalized, distance, _voidLayerMask))
            {
                if (distance > _playerDistance)
                {
                    _mytransform.position += _speed * directionNormalized * Time.deltaTime;
                    _animator.SetBool("Walk", true);
                }
                else
                {
                    _animator.SetBool("Walk", false);
                }
            }
            else
            {
                _animator.SetBool("Walk", false);
            }

        }
        else
        {
            _animator.SetBool("Walk", false);
        }
    }
}

