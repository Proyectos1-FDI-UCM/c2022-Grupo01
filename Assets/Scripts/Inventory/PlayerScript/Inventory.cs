using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            return _instance;
        }
    }
	#endregion

	private void Awake()
	{
        _instance = this;
        int i = 23;
        Debug.Log($"{i} / 10 * 10 % 10 = {(i / 10) * 10 % 10}");
	}

    public List<GameObject> passiveItemList;
    public GameObject activeItem;
    public GameObject bottleItem;
}
