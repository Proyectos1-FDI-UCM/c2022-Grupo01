using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3 : MonoBehaviour
{
	public GameObject textoTutorial;

	void Start()
	{
		textoTutorial.SetActive(false);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		textoTutorial.SetActive(true);
		Destroy(textoTutorial, 5f);
		Destroy(gameObject, 5f);
		Debug.Log("activa texto del tutorial 3");
	}
}
