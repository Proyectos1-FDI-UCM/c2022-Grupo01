using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoTutorial : MonoBehaviour
{
	public GameObject textoTutorial;

	#region parameters
	[SerializeField] 
	private float _tiempoVisible = 5f;
	#endregion
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
			Debug.Log("activa texto tutorial");
			if (Input.anyKeyDown)
			{
				Destroy(textoTutorial);
			}
			else
            {
				Destroy(textoTutorial, _tiempoVisible); // desaparece el objeto activado (caja de texto) en el tiempo definido
			}
		}
	}
}
