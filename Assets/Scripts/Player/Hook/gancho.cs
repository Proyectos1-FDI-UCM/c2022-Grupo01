using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gancho : MonoBehaviour
{
	public PlayerMovement player;

	public void LanzaGancho(Vector3 position, Vector3 travelPoint)
	{
		player.LanzaGancho(position, travelPoint);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		player.gancho = false;
	}
}