using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : Object
{
    public GameObject activePrefab;
    public bool pickable = true;
    public float cooldown = 0;
    [HideInInspector] public float _elapsedTime;
    [HideInInspector] public GameObject sonToCreate;
    [HideInInspector] public bool sonCreated = false;

    public virtual void Activate()
    {

    }

    public virtual void ChangeActiveObject()
    {
        GameObject oldOBject = Instantiate(activePrefab, PlayerManager.Instance._playerPosition, Quaternion.identity);
        oldOBject.name = Inventory.Instance.activeItem.name;
        oldOBject.SetActive(true);
        oldOBject.GetComponent<ActiveObject>().sonCreated = false;
    }
}