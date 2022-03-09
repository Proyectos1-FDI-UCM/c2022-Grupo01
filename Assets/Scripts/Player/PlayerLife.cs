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
        if (healthToAdd < 0) animator.SetTrigger("Hurt");
        if(_playerManager.myLifeState == PlayerManager.LifeStates.Normal || healthToAdd >= 0)
        {
            health += healthToAdd;
            health = Mathf.Clamp(health, 0, maxHealth);
            PlayerManager.Instance.UpdateLife(health);
            GameManager.Instance.ShowHealth(health);
            if (health <= 0)
            {
                Die();
            }
        }
        else if(_playerManager.myLifeState == PlayerManager.LifeStates.HolyFlotador)
        {
            _playerManager.myLifeState = PlayerManager.LifeStates.Normal;   // Si tiene escudos hay que comprobar en que estado está, pero por ahora es así
            HolyFlotadorImage.Instance.enabled = false;
        }
    }

    public void SetMaxHealth(float maxHealthToAdd)
    {
        maxHealth += maxHealthToAdd;
        maxHealth = Mathf.Clamp(maxHealth, 0, 200);
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


    private void Update()
    {
        _playerManager.UpdateLife(health);
        _playerManager.UpdateMaxLife(maxHealth);
        GameManager.Instance.ShowHealth(health);
    }
}
