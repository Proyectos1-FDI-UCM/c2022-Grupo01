using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    #region parameters
    public float health, maxHealth = 100;
    #endregion

    #region references
    [SerializeField]
    private Animator animator;
    private PlayerManager _playerManager;
    #endregion

    #region methods
    public void SetHealth(float healthToAdd)
	{
        if(_playerManager.myLifeState == PlayerManager.LifeStates.Normal || health < health + healthToAdd)
        {
            health += healthToAdd;
            health = Mathf.Clamp(health, 0, maxHealth);
            GameManager.Instance.ShowHealth(health);
            if (health <= 0)
            {
                Die();
            }
            PlayerManager.Instance.UpdateLife(health);
        }
        else if(_playerManager.myLifeState == PlayerManager.LifeStates.HolyFlotador)
        {
            _playerManager.myLifeState = PlayerManager.LifeStates.Normal;   // Si tiene escudos hay que comprobar en que estado está, pero por ahora es así
        }
	}
    private void Die()
	{
        animator.SetBool("Walk", false);
        GetComponent<PlayerMovement>().enabled = false;
        animator.SetTrigger("Die");

        GameManager.Instance.OnPlayerDie();
	}
	#endregion
	// Start is called before the first frame update
	void Start()
    {
        _playerManager = PlayerManager.Instance;
        health = maxHealth;
        GameManager.Instance.ShowHealth(health);
        _playerManager.UpdateLife(health);
        _playerManager.UpdateMaxLife(maxHealth);
    }
}
