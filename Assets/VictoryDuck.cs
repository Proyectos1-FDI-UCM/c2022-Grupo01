using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryDuck : ActiveObject
{
	// Start is called before the first frame update
	public override void Activate()
	{
		PlayerManager.Instance.player.SetActive(false);
	}
}