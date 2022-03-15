using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingEnemyDropController : MonoBehaviour
{
    #region properties
    /// <summary>
    /// Botella a instanciar por el huidizo
    /// </summary>
    private GameObject _instantiatedBottle;
    #endregion

    #region parameters
    /// <summary>
    /// Booleano que comprueba si un huidizo ha instanciado una botella o no
    /// </summary>
    private bool hasInstantiated = false;
    #endregion

    #region references
    /// <summary>
    /// Referencia al EnemyLifeComponent
    /// </summary>
    private EnemyLifeComponent _fleeingEnemyLifeComponent;
    #endregion

    #region methods
    /// <summary>
    /// Métod. que instancia una botella cuando un enemigo huidizo muere (CAMBIAR UTILIZANDO UNA LISTA DE BOTELLAS NUEVA)
    /// </summary>
    public void InstantiateBottleOnDie()
    {
        int rnd = Random.Range(0, 3);
        _instantiatedBottle = GameManager.Instance.itemList[rnd];
        Instantiate(_instantiatedBottle, transform.position, Quaternion.identity);
    }
    #endregion

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
