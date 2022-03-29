using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemy : MonoBehaviour
{
    public NecromancerController _necromancer;

    // Start is called before the first frame update
	private void OnDestroy()
	{
        _necromancer._weakCounter--;
	}
}
