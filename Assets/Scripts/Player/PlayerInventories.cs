using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventories : MonoBehaviour
{
    public Inventory activeInventory;
    public Inventory passiveInventory;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Detectado un item: " + collision.name);
        var item = collision.GetComponent<PhysicalItem>();
        if (item)
        {
            Destroy(collision.gameObject);
            if(item.item.type == ItemType.Passive)
            {
                passiveInventory.AddPassiveItem(new Item(item.item));
            }
            else
            {
                activeInventory.AddActiveItem(new Item(item.item));
            }
        }
    }

    private void OnApplicationQuit()
    {
        passiveInventory.inventoryContainer.itemListInInventory.Clear();
        activeInventory.inventoryContainer.itemListInInventory.Clear();
    } 
}
