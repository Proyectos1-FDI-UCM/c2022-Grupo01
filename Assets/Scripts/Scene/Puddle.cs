using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
	#region parameters
	[SerializeField]
	private float _touchHydrate = 10;
	#endregion


	private void OnTriggerEnter2D(Collider2D collision)
	{
        PlayerLife player = collision.gameObject.GetComponent<PlayerLife>();

        if(player != null)  //&& health <= 90
							// mas adelante solo si esta rodando
		{
			player.SetHealth(+_touchHydrate);
		}
	}
}