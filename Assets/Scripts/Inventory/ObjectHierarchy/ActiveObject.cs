using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : Object
{
    public int maxUses;
    public GameObject activePrefab;
    public bool pickable = true;
    public float cooldown = 0;
    [HideInInspector] public int uses;
    [HideInInspector] public float _elapsedTime;
    [HideInInspector] public GameObject sonToCreate;
    [HideInInspector] public bool sonCreated = false;


    public virtual void Activate()
    {
        Debug.Log("Objeto activo " + gameObject.name + " activado");
    }

    public virtual void ChangeActiveObject()
    {
        Debug.Log("Cambiado Objeto Activo");
        GameObject oldOBject = Instantiate(activePrefab, PlayerManager.Instance._playerPosition, Quaternion.identity);
        oldOBject.name = Inventory.Instance.activeItem.name;
        oldOBject.SetActive(true);
    }

    public void UpdateUses(int newUses)
	{
        uses = newUses;
	}
}