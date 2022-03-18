using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleObject : ActiveObject
{
    public override void ChangeActiveObject()
    {
        base.ChangeActiveObject();
    }

    void Start()
    {
        type = ItemTypes.Bottle;
    }
}
