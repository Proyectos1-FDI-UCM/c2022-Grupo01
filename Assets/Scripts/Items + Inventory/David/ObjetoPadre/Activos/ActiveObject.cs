using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : Object
{
    [HideInInspector] public int uses;
    public int maxUses;
    public GameObject activePrefab;
    //Meter atributos aquí
    public virtual void Activate()
    {
        Debug.Log("Objeto activo " + gameObject.name + " activado");
    }

    public virtual void ChangeActiveObject()
    {
        Debug.Log("Cambiado Objeto Activo");
    }
}
