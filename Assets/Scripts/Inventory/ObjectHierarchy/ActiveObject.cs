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

        if (type == ItemTypes.Active)
        {  
            oldOBject.name = Inventory.Instance.activeItem.name;
            oldOBject.SetActive(true);
            oldOBject.GetComponent<ActiveObject>().sonCreated = false;
        }
        else if(type == ItemTypes.Bottle)
        {
            oldOBject.name = Inventory.Instance.bottleItem.name;
            oldOBject.SetActive(true);
        }
    }

    public virtual void OnNewFloor()
	{
        //recover cooldown
        //recover pools 
	}
}