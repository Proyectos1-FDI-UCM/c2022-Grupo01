using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Active, Passive }

public abstract class ItemObject : ScriptableObject
{
    #region ItemProperties
    public Sprite sprite;
    public ItemType type;
    public int itemID;
    [TextArea(1,2)] public string shortDescription;
    [TextArea(4, 5)] public string longDescription;
    #endregion
}

[System.Serializable]
public class Item
{
    public string Name;
    public Sprite sprite;
    public int itemID;
    public Item(ItemObject item)
    {
        Name = item.name;
        itemID = item.itemID;
        sprite = item.sprite;
    }
}