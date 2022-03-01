using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo inventario", menuName = "Inventario/Crear Inventario")]
public class Inventory : ScriptableObject
{
    public InventoryList inventoryContainer;

    public void AddItem(Item _item)
    {
        bool hasItem = false;

        for(int i = 0; i < inventoryContainer.itemListInInventory.Count; i++)
        {
            if(inventoryContainer.itemListInInventory[i].item == _item)
            {
                hasItem = true;
                break;
            }
        }
        if(!hasItem) {
            inventoryContainer.itemListInInventory.Add(new InventorySlot(_item.itemID, _item));
        }
    }
}

[System.Serializable]
public class InventoryList
{
    public List<InventorySlot> itemListInInventory = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public InventorySlot(int _id, Item _item)
    {
        ID = _id;
        item = _item;
    }
}