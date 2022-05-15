using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollController : MonoBehaviour
{
	#region references
	Rigidbody2D rb;
	PlayerMovement playerMovement;
	#endregion

	private void OnTriggerEnter2D(Collider2D collision)
	{
		playerMovement.canRoll = false;
		playerMovement.EndRoll();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		playerMovement.canRoll = false;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		playerMovement.canRoll = true;
	}

	private void Start()
	{
		playerMovement = GetComponentInParent<PlayerMovement>();
		rb = GetComponentInParent<Rigidbody2D>();
		playerMovement.canRoll = true;
	}

	private void Update()
	{
		Vector2 lookDir = playerMovement.animationDirection;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
		rb.rotation = angle;
		rb.position = PlayerManager.Instance._playerPosition;
	}
}