using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo item pasivo", menuName = "Inventario/Items/Pasivo")]
public class PassiveItem : ItemObject
{
    public void Awake()
    {
        type = ItemType.Passive;
    }
}