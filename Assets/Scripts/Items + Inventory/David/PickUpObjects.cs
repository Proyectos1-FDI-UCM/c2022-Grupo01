using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [SerializeField] InventoryDavid inventory;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.F))
        {
            PassiveItemDavid passiveItem = other.GetComponent<PassiveItemDavid>();
        
            if (passiveItem != null)
            {
                inventory.AddPassiveItem(other.gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                ActiveItemDavid activeItem = other.GetComponent<ActiveItemDavid>();

                if (activeItem != null)
                {
                    Debug.Log("Detectaditto");
                    inventory.AddActiveItem(other.gameObject);
                    Destroy(other.gameObject);
                }
                else
                {
                    EcoBottleDavid ecoBottle = other.GetComponent<EcoBottleDavid>();

                    if (ecoBottle != null)
                    {
                    inventory.AddEcoBottle(other.gameObject);
                    Destroy(other.gameObject);
                    }
                }
            }
        }
    }
}
