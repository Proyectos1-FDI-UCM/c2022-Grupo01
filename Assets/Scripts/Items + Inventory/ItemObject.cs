using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Active, Passive }

public enum ItemAttributes {Health, MaxHealth, Speed, Damage}

public abstract class ItemObject : ScriptableObject
{
    #region ItemProperties
    public Sprite sprite;
    public ItemType type;
    public int itemID;
    public GameObject itemPrefab;
    [TextArea(1,2)]public string shortDescription;
    [TextArea(4, 5)] public string longDescription;
    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
    #endregion
}

[System.Serializable]
public class Item
{
    public string Name;
    public Sprite sprite;
    public int itemID;
    public GameObject itemPrefab;
    public ItemBuff[] buffs;
    public Item(ItemObject item)
    {
        Name = item.name;
        itemID = item.itemID;
        sprite = item.sprite;
        itemPrefab = item.itemPrefab;
        buffs = new ItemBuff[item.buffs.Length];
        for(int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].value);
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public ItemAttributes attribute;
    public int value;
    public ItemBuff(int _value)
    {
        value = _value;
    }
}