using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo inventario", menuName = "Inventario/Crear Inventario")]
public class Inventory : ScriptableObject
{
    public InventoryList inventoryContainer;
    public Transform cosa2;

    public void AddItem(Item _item)
    {
        bool hasItem = false;

        int i = 0;
        while (i < inventoryContainer.itemListInInventory.Count && !hasItem)
        {
            if (inventoryContainer.itemListInInventory[i].item == _item)
            {
                hasItem = true;
            }
            i++;
        }
        if (!hasItem) 
        {
            inventoryContainer.itemListInInventory.Add(new InventorySlot(_item.itemID, _item));
        }
    }

    /*public void AddActiveItem(Item _item)
    {
        bool hasItem = false;
        int i = 0;
        while (i < inventoryContainer.itemListInInventory.Count && !hasItem)
        {
            if (inventoryContainer.itemListInInventory[i].item == _item)
            {
                hasItem = true;
            }
            i++;
        }

        Instantiate(cosa, cosa2.position, Quaternion.identity);
        if (!hasItem)
        {
            inventoryContainer.itemListInInventory.Add(new InventorySlot(_item.itemID, _item));
        }
    }
    */
    public void ReplaceItem(Item _item)
    {
        for(int i = 0; i < inventoryContainer.itemListInInventory.Count; i++)
        {
            
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
[System.Serializable]
public class ItemToInstantiate
{
    public ScriptableObject _itemPrefabToInstantiate;
}