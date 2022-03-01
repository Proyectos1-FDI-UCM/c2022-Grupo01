using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDavid : MonoBehaviour
{
    [SerializeField] private List<GameObject> passiveInventory;
    [SerializeField] private GameObject activeItem;
    [SerializeField] private GameObject ecoBottle;
    [SerializeField] private Vector3 offset = new Vector3(0,0.5f,0);

    public void AddPassiveItem(GameObject newItem)
    {
        passiveInventory.Add(newItem);
    }

    public void AddActiveItem(GameObject newItem)
    {
        if(activeItem == null)
        {
            Debug.Log("Detectadito");
            activeItem = newItem;
        }
        else
        {
            Debug.Log("Detectadito");
            Instantiate(activeItem, transform.position + offset, Quaternion.identity);
            activeItem = newItem;
        }
    }

    public void AddEcoBottle(GameObject newEcoBottle) 
    { 
        if (newEcoBottle == null)
        {
            ecoBottle = newEcoBottle;
        }
        else
        {
            Instantiate(ecoBottle, transform.position + offset, Quaternion.identity);
            ecoBottle = newEcoBottle;
        }
    }
}
