using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
	#region references
	[SerializeField] private GameObject upperDoor, upperCorridor, lowerDoor, lowerCorridor, leftDoor, leftCorridor, rightDoor, rightCorridor;
	#endregion

	private void Awake()
	{
		upperCorridor.SetActive(false);
		lowerCorridor.SetActive(false);
		leftCorridor.SetActive(false);
		rightCorridor.SetActive(false);
	}

	public void Left(bool thing)
	{
		leftCorridor.SetActive(true);
		leftDoor.SetActive(false);
	}
	public void Right(bool thing)
	{
		rightCorridor.SetActive(true);
		rightDoor.SetActive(false);
	}
	public void Upper(bool thing)
	{
		upperCorridor.SetActive(true);
		upperDoor.SetActive(false);
	}
	public void Lower(bool thing)
	{
		lowerCorridor.SetActive(true);
		lowerDoor.SetActive(false);
	}
}
