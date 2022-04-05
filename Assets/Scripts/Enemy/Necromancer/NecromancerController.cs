using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerController : MonoBehaviour
{
    private Transform _myTransform;
    [SerializeField]
    private float _currentTime, _timeLeft = 3;
    //public float add;

    [SerializeField]
    private GameObject _weakEnemy;
    private GameObject _weakEnemyInstance;

    #region properties
    [HideInInspector] public int _weakCounter;
    #endregion

    #region methods
    public void ExecuteNecromancerController()
    {
        this.enabled = true;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime < 0 && _weakCounter < 4)
        {
            _weakEnemyInstance = Instantiate(_weakEnemy, _myTransform.position, Quaternion.identity);
            _weakEnemyInstance.GetComponent<WeakEnemy>()._necromancer = this;
            FindObjectOfType<AudioManager>().Play("NecromancerSummon");
            _currentTime = _timeLeft;
            _weakCounter++;
        }

        else if (_weakCounter >= 4)
        {
            GetComponent<RangeAttack>().enabled = true;
        }

        else GetComponent<RangeAttack>().enabled = false;
    }
}