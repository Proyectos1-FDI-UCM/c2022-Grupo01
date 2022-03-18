using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    #region parameters
    public float health, maxHealth = 100;
    private bool _invulnerability = false;
    [SerializeField] private float _invulnerabilityTime = 2f;
    #endregion

    #region references
    [SerializeField]
    private Animator animator;
    private PlayerManager _playerManager;
    private PlayerAttack _playerAttack;
    #endregion

    #region properties
    private int _shields;
    #endregion

    #region methods
    public void SetHealth(float healthToAdd, bool isShot)
	{
        if(_invulnerability == false && _playerManager.myLifeState == PlayerManager.LifeStates.Normal || healthToAdd >= 0)
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
            _playerManager.myLifeState = PlayerManager.LifeStates.Normal;   // Si tiene escudos hay que comprobar en que estado est�, pero por ahora es as�
            HolyFlotadorImage.Instance.enabled = false;
        }
        else if (_playerManager.myLifeState == PlayerManager.LifeStates.Shield && healthToAdd < 0)
        {
            _shields--;
            if (_shields <= 0) _playerManager.myLifeState = PlayerManager.LifeStates.Normal;
        }

        if (_invulnerability == false && healthToAdd < 0 && !isShot) 
        {
            StartCoroutine("GetInvulnerable");
            animator.SetTrigger("Hurt");
        }
    }

    public void SetShields(int numberOfShields)
    {
        _playerManager.myLifeState = PlayerManager.LifeStates.Shield;
        _shields += numberOfShields;
        //set HUD (ALEX)
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

    IEnumerator GetInvulnerable()
    {
        _invulnerability = true;
        yield return new WaitForSeconds(_invulnerabilityTime);
        _invulnerability = false;
    }
	#endregion
	
	void Start()
    {
        _playerManager = PlayerManager.Instance;
        health = maxHealth;
        GameManager.Instance.ShowHealth(health);
        _playerManager.UpdateLife(health);
        _playerManager.UpdateMaxLife(maxHealth);
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        _playerManager.UpdateLife(health);
        _playerManager.UpdateMaxLife(maxHealth);
        GameManager.Instance.ShowHealth(health);
    }
}
