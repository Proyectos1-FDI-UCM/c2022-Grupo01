using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveObject : Object
{
    public int maxUses;
    public GameObject activePrefab;
    public bool pickable = true;
    public Slider cooldownSlider;

    private void Start()
    {

    }

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
}