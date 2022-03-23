using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleObject : ActiveObject
{
    public override void ChangeActiveObject()
    {
        base.ChangeActiveObject();
    }

    public void RemoveBottle()
    {
        Destroy(Inventory.Instance.bottleItem);
    }

    void Start()
    {
        type = ItemTypes.Bottle;
    }
}
