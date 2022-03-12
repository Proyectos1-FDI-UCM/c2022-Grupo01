using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoTutorial : MonoBehaviour
{
	public GameObject textoTutorial;


	void Start()
	{
		textoTutorial.SetActive(false);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerLife player = collision.gameObject.GetComponent<PlayerLife>();
		if (player != null)
		{
			textoTutorial.SetActive(true);
			Destroy(textoTutorial, 5f);
			Destroy(gameObject, 5f);
			Debug.Log("activa texto tutorial");
		}
	}

}
