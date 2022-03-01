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

	private void OnTriggerStay2D(Collider2D collision)
	{
        PlayerLife player = collision.gameObject.GetComponent<PlayerLife>();
		if (player != null && _playerManager.playerInRoll)
		{
			player.SetHealth(+_touchHydrate);
			DestroyPuddle();
		}
	}

	public void DestroyPuddle()
    {
		_spriteRenderer.sprite = newSprite;
		GetComponent<Collider2D>().enabled = false;
		Destroy(gameObject, 3f);
	}
}