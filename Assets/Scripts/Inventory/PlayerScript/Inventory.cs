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
	}

    public List<GameObject> passiveItemList;
    public GameObject activeItem;
    public List<GameObject> bottleList;
}
