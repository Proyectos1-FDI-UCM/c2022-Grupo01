using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2 : MonoBehaviour
{
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

			_displayManager.DisplayMessage("Tutorial 2");

			//Destroy(gameObject);
	}
	*/
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("activa texto del tutorial 2");
	}
}
