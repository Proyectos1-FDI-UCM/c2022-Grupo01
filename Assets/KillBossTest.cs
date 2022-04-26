using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBossTest : MonoBehaviour
{
	#region references
	[SerializeField] private GameObject _boss;
	#endregion
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) Destroy(_boss);
    }
}
