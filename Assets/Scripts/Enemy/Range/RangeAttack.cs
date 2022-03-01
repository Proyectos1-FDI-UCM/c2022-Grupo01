using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    #region properties
    private Vector3 _lastPosition;
    private Vector3 _shotPosition;
    private float _elapsedTime;
    #endregion
    #region parameters
    [SerializeField]
    private float _shotForce, _cooldownTime, _attackOffset;
    #endregion
    #region methods
    public void ExecuteRangeAttack()
    {
        this.enabled = true;
    }
    private void Shoot()
    {
        Vector3 direction = (_shotPosition - _myTransform.position).normalized;
        GameObject bullet = Instantiate(_bulletPrefab, _myTransform.position + direction, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(bullet.transform.rotation.x, 0, 0);
        rb.AddForce(direction * _shotForce, ForceMode2D.Impulse);
    }
    #endregion
    #region references
    [SerializeField]
    private GameObject _bulletPrefab;
    private PlayerManager _myPlayerManager;
    [SerializeField]
    private Transform _myTransform;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _myPlayerManager = PlayerManager.Instance;
        _elapsedTime = 0f;
        _lastPosition = _myPlayerManager._playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > _cooldownTime)
        {
            Vector3 direction = (_myPlayerManager._playerPosition - _lastPosition).normalized;
            _shotPosition = _myPlayerManager._playerPosition + direction * _attackOffset;
            Shoot();
            _elapsedTime = 0f;
        }
        _lastPosition = _myPlayerManager._playerPosition;
    }
    
}