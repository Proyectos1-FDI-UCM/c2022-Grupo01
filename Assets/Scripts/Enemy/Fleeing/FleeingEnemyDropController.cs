using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingEnemyDropController : MonoBehaviour
{
    #region parameters
    private bool hasInstantiated = false;
    #endregion

    #region properties
    public GameObject[] bottles;
    public GameObject blueBottle, yellowBottle, greenBottle;
    #endregion

    #region references
    private EnemyLifeComponent _fleeingEnemyLifeComponent;
    #endregion

    #region methods
    public void InstantiateBottleOnDie()
    {
        int bottleType = Random.Range(0, 3);

        Instantiate(bottles[bottleType], transform.position, Quaternion.identity);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _fleeingEnemyLifeComponent = GetComponent<EnemyLifeComponent>();
        bottles = new GameObject[3] {blueBottle, yellowBottle, greenBottle};
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
