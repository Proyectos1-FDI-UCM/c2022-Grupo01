using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHurtComponent : MonoBehaviour
{
	#region parameters
	[SerializeField]
	private float _touchDamage = 10;
	#endregion

	private void OnTriggerEnter2D(Collider2D collision)
	{
        PlayerLife player = collision.gameObject.GetComponent<PlayerLife>();

        if (player != null)
		{
			PlayerManager.Instance.ChangePlayerLife(-_touchDamage);
		}
	}


}