using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseActiveObject : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
		{
            Inventory.Instance.activeItem.GetComponent<ActiveObject>().Activate();
		}
    }
}