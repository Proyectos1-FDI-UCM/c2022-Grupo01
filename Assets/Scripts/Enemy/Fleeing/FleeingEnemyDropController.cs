using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingEnemyDropController : MonoBehaviour
{
    #region properties
    private GameObject _instantiatedBottle;
    #endregion

    #region parameters
    private bool hasInstantiated = false;
    #endregion

    #region references
    private EnemyLifeComponent _fleeingEnemyLifeComponent;
    #endregion

    #region methods
    public void InstantiateBottleOnDie()
    {
        int rnd = Random.Range(0, 3);
        _instantiatedBottle = GameManager.Instance.itemList[rnd];
        Instantiate(_instantiatedBottle, transform.position, Quaternion.identity);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _fleeingEnemyLifeComponent = GetComponent<EnemyLifeComponent>();
    }

    void Update()
    {
        if (_fleeingEnemyLifeComponent._currentLife <= 0 && !hasInstantiated)
        {
            hasInstantiated = true;
            InstantiateBottleOnDie();
        }
    }
}
