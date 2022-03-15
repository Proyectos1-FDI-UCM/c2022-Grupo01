using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
	#region parameters
	/// <summary>
    /// Cooldown de los enemigos a melee
    /// </summary>
	[SerializeField] private float _meleeCooldown = 0.8f;

	/// <summary>
    /// Rango de ataque de los enemigos a melee
    /// </summary>
	[SerializeField] private float _attackRange = 2f;

	/// <summary>
    /// Daño de los enemigos a melee
    /// </summary>
	[SerializeField] private int _meleeDamage = 20;
	#endregion

	#region references
	/// <summary>
    /// Referencia al PlayerManager
    /// </summary>
	private PlayerManager _myPlayerManager;

	/// <summary>
	/// Referencia al LayerMask del jugador
	/// </summary>
	[SerializeField] private LayerMask _playerLayer;

	/// <summary>
    /// Referencia al Animator
    /// </summary>
	[SerializeField] private Animator _animator;
	#endregion

	#region methods
	/// <summary>
    /// Activa el script de ataque a melee
    /// </summary>
	public void ExecuteMeleeAttack()
    {
		this.enabled = true;
    }

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, _attackRange);
	}

	/// <summary>
    /// Ataque a melee del enemigo
    /// </summary>
	private void Melee()
	{
		_animator.SetTrigger("Attack");

		Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, _attackRange, _playerLayer);

		if(hitPlayer[0] != null && hitPlayer[0].isTrigger!=true) PlayerManager.Instance.ChangePlayerLife(-_meleeDamage);
		
	}
    #endregion
    private void Start()
    {
		_myPlayerManager = PlayerManager.Instance;
    }

    private void FixedUpdate()
	{
		//checkear distancia entre PJ y enemigo
		float distance = Vector3.Magnitude(_myPlayerManager._playerPosition - transform.position);
		_meleeCooldown -= Time.fixedDeltaTime;
		if (distance < 2 && _meleeCooldown <= 0)
		{
			Melee();
			_meleeCooldown = 0.8f;
		}
	}
	
}
