using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
	#region parameters
	[SerializeField] private float _pickUpRange = 2f;
    [SerializeField] private LayerMask _objectLayer;
	#endregion

	#region properties
	private bool activeObjectPickedUp = false;
    private bool bottleObjectPickedUp = false;
	#endregion
	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
		{
            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, _pickUpRange, _objectLayer);
            if(items.Length >= 1) PickUpObject(items[0]);
        }
    }

    void PickUpObject(Collider2D item)
	{
        Object objeto = item.GetComponent<Object>();

        if(objeto != null)
        {
            if (objeto.type == ItemTypes.Passive)
            {
                Inventory.Instance.passiveItemList.Add(item.gameObject);
                item.GetComponent<PassiveObject>().Activate();
                GameManager.Instance.ObjectInfo(item.GetComponent<PassiveObject>().nameOnScreen, item.GetComponent<PassiveObject>().littleDescriptionOnScreen);
            }
            else if (objeto.type == ItemTypes.Active)
            {
                if (activeObjectPickedUp) Inventory.Instance.activeItem.GetComponent<ActiveObject>().ChangeActiveObject();
                Destroy(Inventory.Instance.activeItem);
                Inventory.Instance.activeItem = item.gameObject.GetComponent<ActiveObject>().activePrefab;
                ActiveInventoryPanelManager.Instance.UpdateActiveDisplay();
                item.GetComponent<ActiveObject>().pickable = false;
                activeObjectPickedUp = true;
                GameManager.Instance.SetCooldownBar(true);
                GameManager.Instance.ObjectInfo(Inventory.Instance.activeItem.GetComponent<ActiveObject>().nameOnScreen, Inventory.Instance.activeItem.GetComponent<ActiveObject>().littleDescriptionOnScreen);
            }
            else if (objeto.type == ItemTypes.Bottle)
            {
                if (bottleObjectPickedUp) Inventory.Instance.bottleItem.GetComponent<BottleObject>().ChangeActiveObject();
                Destroy(Inventory.Instance.bottleItem);
                Inventory.Instance.bottleItem = item.gameObject.GetComponent<BottleObject>().activePrefab;
                //CREAR PANELMANAGER DE BOTELLAS
                item.GetComponent<BottleObject>().pickable = false;
                bottleObjectPickedUp = true;
                GameManager.Instance.ObjectInfo(Inventory.Instance.bottleItem.GetComponent<BottleObject>().nameOnScreen, Inventory.Instance.bottleItem.GetComponent<BottleObject>().littleDescriptionOnScreen);
            }
            item.gameObject.SetActive(false);
        }
    }
}