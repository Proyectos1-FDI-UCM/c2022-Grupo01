using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTry2 : MonoBehaviour
{
    #region Singleton
    private static InventoryTry2 _instance;
    public static InventoryTry2 Instance
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
}
