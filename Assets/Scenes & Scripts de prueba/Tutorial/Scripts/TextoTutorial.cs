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
			Destroy(gameObject); // elimina el trigger para no volver a activarse al pasar de vuelta
			textoTutorial.SetActive(true);
			Destroy(textoTutorial, 5f); // desaparece el objeto activado (caja de texto) en 5seg
			Debug.Log("activa texto tutorial");
			/*
			if (Input.anyKeyDown)
			{
				Destroy(textoTutorial);
			}
			*/
		}
	}

}
