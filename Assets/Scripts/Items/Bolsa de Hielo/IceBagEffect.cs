using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceBagEffect : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _attackRange = 2;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _iceDamage = 30;
    [SerializeField] private float _timeToDestroy = 5f;
    #endregion

    #region properties
    private float _elapsedTime;
    Collider2D[] hitPlayer;
    #endregion

    private void Start()
    {
        hitPlayer = Physics2D.OverlapCircleAll(transform.position, _attackRange, _enemyLayer);

        foreach (Collider2D enemy in hitPlayer)
        {
            try { enemy.GetComponent<EnemyLifeComponent>().Damage(_iceDamage); }
            catch { Debug.Log(enemy.name + " no quiere ser golpeado"); }
            if (enemy.GetComponent<MeleeMovement>() != null) 
            {
                enemy.GetComponent<MeleeMovement>().speed /= 2;
            }
            else if (enemy.GetComponent<FleeingEnemyMovement>() != null) 
            {
                enemy.GetComponent<FleeingEnemyMovement>()._speed /= 2;
            }
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _timeToDestroy)
        {
            foreach (Collider2D enemy in hitPlayer)
            {
                if (enemy.GetComponent<MeleeMovement>() != null) enemy.GetComponent<MeleeMovement>().speed *= 2;
                else if (enemy.GetComponent<FleeingEnemyMovement>() != null) enemy.GetComponent<FleeingEnemyMovement>()._speed *= 2;
                else if (enemy.GetComponent<RangeMovement>() != null) enemy.GetComponent<RangeMovement>().speed *= 2;
            }
            Destroy(gameObject);
        }
    }
}
