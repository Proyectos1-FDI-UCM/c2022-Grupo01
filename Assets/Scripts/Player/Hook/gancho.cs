using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gancho : MonoBehaviour
{
	public PlayerMovement player;

	public void LanzaGancho(Vector3 position)
	{
		player.LanzaGancho(position);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		ContactPoint2D punto = collision.GetContact(0);
		player.gancho = false;
	}
}