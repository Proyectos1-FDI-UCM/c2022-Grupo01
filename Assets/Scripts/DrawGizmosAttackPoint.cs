using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmosAttackPoint : MonoBehaviour
{
    public Transform _attackPoint;
    public float _attackRange;
    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

}
