using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
	#region parameters
	public float _touchHydrate = 10;
	#endregion

	#region references
	private PlayerManager _playerManager;
	private SpriteRenderer _spriteRenderer;
	public Sprite newSprite;
	#endregion

	void Start()
	{
		_playerManager = PlayerManager.Instance;
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        PlayerLife player = collision.gameObject.GetComponent<PlayerLife>();
		if (player != null && _playerManager.playerInRoll)
		{
			AudioManager.Instance.Play("Puddle");
			player.SetHealth(+_touchHydrate, false);
			UsedPuddle();
		}
	}

	public void UsedPuddle()
    {
		GetComponent<Collider2D>().enabled = false;
		_spriteRenderer.sprite = newSprite;
		Destroy(gameObject, 3f);
	}
}
