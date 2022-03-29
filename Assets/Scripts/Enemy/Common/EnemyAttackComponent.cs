using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackComponent : MonoBehaviour
{
    #region parameters
	/// <summary>
    /// Daño a contacto de los enemigos
    /// </summary>
    [SerializeField] private float _touchDamage = 30;
    #endregion

    #region methods
    private void OnCollisionEnter2D(Collision2D collision)
	{
        PlayerLife player = collision.gameObject.GetComponent<PlayerLife>();

        if (player != null)
        {
            PlayerManager.Instance.ChangePlayerLife(-_touchDamage, false);
        }
        
	}
    #endregion
}