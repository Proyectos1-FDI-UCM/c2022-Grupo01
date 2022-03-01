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
        if(_playerManager.myLifeState == PlayerManager.LifeStates.Normal || healthToAdd > 0)
        {
            health += healthToAdd;
            health = Mathf.Clamp(health, 0, maxHealth);
            if (health <= 0)
            {
                Die();
            }
            PlayerManager.Instance.UpdateLife(health);
            GameManager.Instance.ShowHealth(health);
        }
        else if(_playerManager.myLifeState == PlayerManager.LifeStates.HolyFlotador)
        {
            _playerManager.myLifeState = PlayerManager.LifeStates.Normal;   // Si tiene escudos hay que comprobar en que estado est�, pero por ahora es as�
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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SetHealth(10);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            maxHealth += 10;
        }
        _playerManager.UpdateLife(health);
        _playerManager.UpdateMaxLife(maxHealth);
        GameManager.Instance.ShowHealth(health);
    }



}
