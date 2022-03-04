using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [SerializeField] private float _pickUpRange = 2f;
    [SerializeField] private LayerMask _objectLayer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
		{
            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, _pickUpRange, _objectLayer);

            PickUpObject(items[0]);
        }
    }

    void PickUpObject(Collider2D item)
	{
        if (item.GetComponent<ActiveObject>() == null) InventoryTry2.Instance.passiveItemList.Add(item.gameObject);
        else Debug.Log("UWU");
        {
            if (InventoryTry2.Instance.activeItem != null) InventoryTry2.Instance.activeItem.GetComponent<ActiveObject>().ChangeActiveObject();
            InventoryTry2.Instance.activeItem = item.gameObject;
            
        }

        item.gameObject.SetActive(false);
	}
}
