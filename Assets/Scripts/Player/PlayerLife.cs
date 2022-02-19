using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _health, _maxHealth = 100;
    #endregion

    #region references
    public Animator animator;
    #endregion

    #region methods
    public void SetHealth(float healthToAdd)
	{
        _health += healthToAdd;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        GameManager.Instance.ShowHealth(_health);
        if(_health <= 0)
		{
            Die();
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
        _health = _maxHealth;
        GameManager.Instance.ShowHealth(_health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
