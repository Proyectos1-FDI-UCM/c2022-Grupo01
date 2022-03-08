using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
	public GameObject textoTutorial;
	/*
	#region references
	private DisplayManager _displayManager;
	#endregion


	void Start()
	{
		_displayManager = DisplayManager.Instance();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

			_displayManager.DisplayMessage("Tutorial 1");

			//Destroy(gameObject);
	}
	*/

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
			Debug.Log("activa texto");
		}
	}

}
