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
    //DEFINIR LOS ATRIBUTOS DE CADA OBJETO
    public float healthBonus;
    public float maxHealthBonus;
    public float speedBonus;
    public float attackBonus;
    public MonoBehaviour uwu;
}