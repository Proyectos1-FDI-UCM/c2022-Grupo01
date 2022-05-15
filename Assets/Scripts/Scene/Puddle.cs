using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
	#region parameters
	public float _touchHydrate = 10;
	#endregion
	#region properties
	[HideInInspector] public bool activated = false;
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
	public void UsedPuddle()
    {
		GetComponent<Collider2D>().enabled = false;
		_spriteRenderer.sprite = newSprite;
		Destroy(gameObject, 3f);
	}
}
