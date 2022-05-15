using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gancho : MonoBehaviour
{
	public PlayerMovement player;
	[SerializeField] private float distance = 20;
	public float speed = 10;
	[HideInInspector] public float hookSpeed;
	[HideInInspector] public bool launching = false, canLaunch = true, canMove = true;
	public IEnumerator LaunchHook(Vector3 position)
	{
		launching = true;
		int i = 0;
		Vector2 dir = (position - transform.position).normalized;
		while (i < distance)
		{
			transform.Translate(dir * hookSpeed * Time.deltaTime);
			i++;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		Comeback();
	}

	void Comeback()
	{
		launching = false;
	}

	private void Start()
	{
		hookSpeed = speed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		PosteDetector poste = collision.GetComponent<PosteDetector>();

		if (collision.gameObject != player.gameObject && poste == null)
		{
			StopCoroutine("LaunchHook");
			Comeback();
		}
		else if (poste != null)
		{
			player.StartCoroutine(player.MovePlayerToHookPoint(poste.travelPoint.position));
		}
	}

	private void Update()
	{
		if (canMove)
		{
			if (Vector3.Magnitude(transform.position - PlayerManager.Instance._playerPosition) < 0.2) { canLaunch = true; gameObject.SetActive(false); }
			else { canLaunch = false; }
		}
	}
}