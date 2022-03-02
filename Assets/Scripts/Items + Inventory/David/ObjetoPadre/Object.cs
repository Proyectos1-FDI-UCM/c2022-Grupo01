using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes { Buff, NoBuff,Active }

public class Object : MonoBehaviour
{
    public int ID;

    
}

    /*    public Item(ItemObject item)
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
}*/