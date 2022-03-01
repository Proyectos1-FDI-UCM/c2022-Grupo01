using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
	#region parameters
	[SerializeField]
	private float _meleeCooldown = 0.8f;
	[SerializeField]
	private float _attackRange = 2f;
	[SerializeField]
	private int _meleeDamage = 20;
	[SerializeField]
	private LayerMask _playerLayer;
	[SerializeField]
	private Animator _animator;
	#endregion
	#region references
	private PlayerManager _myPlayerManager;
	#endregion
	#region methods
	public void ExecuteMeleeAttack()
    {
		this.enabled = true;
    }
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, _attackRange);
	}
	private void Melee()
	{
		_animator.SetTrigger("Attack");

		Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(transform.position, _attackRange, _playerLayer);

		foreach (Collider2D player in hitPlayer)
		{
			player.gameObject.GetComponent<PlayerLife>().SetHealth(-_meleeDamage);
		}
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
		_meleeCooldown -= Time.deltaTime;
		if (distance < 2 && _meleeCooldown <= 0)
		{
			Melee();
			_meleeCooldown = 0.8f;
		}
	}
	
}
