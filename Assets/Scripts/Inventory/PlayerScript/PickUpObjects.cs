using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [SerializeField] private float _pickUpRange = 2f;
    [SerializeField] private LayerMask _objectLayer;

    private bool activeObjectPickedUp = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
		{
            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, _pickUpRange, _objectLayer);

            //if(items.Length >= 1) PickUpObject(items[0]);
            if(items.Length >= 1) PickUpObject(items[0]);
        }
    }

    void PickUpObject(Collider2D item)
	{
        if (item.GetComponent<PassiveObject>() != null) 
        { 
            Inventory.Instance.passiveItemList.Add(item.gameObject); 
        }
        else if (item.GetComponent<ActiveObject>() != null)
        {
            if (activeObjectPickedUp) Inventory.Instance.activeItem.GetComponent<ActiveObject>().ChangeActiveObject();
            Destroy(Inventory.Instance.activeItem);
            Inventory.Instance.activeItem = item.gameObject.GetComponent<ActiveObject>().activePrefab;
            item.GetComponent<ActiveObject>().pickable = false;
            activeObjectPickedUp = true;
        }
        item.gameObject.SetActive(false);
        //else if (item.GetComponent<ActiveObject>() != null)
    }
}