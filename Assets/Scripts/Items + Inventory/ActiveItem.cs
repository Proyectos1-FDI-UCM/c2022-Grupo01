using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo item activo", menuName = "Inventario/Items/Activo")]
public class ActiveItem : ItemObject
{
    public void Awake()
    {
        type = ItemType.Active;
    }
    //DEFINIR LOS ATRIBUTOS DE CADA OBJETO
}
