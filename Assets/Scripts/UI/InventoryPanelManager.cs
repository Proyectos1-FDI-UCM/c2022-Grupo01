using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelManager : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;

    #region parameters
    [SerializeField] private int _spaceBetweenItemsX, _spaceBetweenItemsY, _columns, _xStart, _yStart;
    [SerializeField] private GameObject inventoryPrefab;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    #endregion

    public void CreateDisplay()
    {
        for (int i = 0; i < _inventory.inventoryContainer.itemListInInventory.Count; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            itemsDisplayed.Add(_inventory.inventoryContainer.itemListInInventory[i], obj);
        }
    }

    public void UpdateDisplay()
    {
        for(int i = 0; i < _inventory.inventoryContainer.itemListInInventory.Count; i++)
        {
            InventorySlot slot = _inventory.inventoryContainer.itemListInInventory[i];

            if(!itemsDisplayed.ContainsKey(slot))
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = slot.item.sprite;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                itemsDisplayed.Add(slot, obj);
            }
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(_xStart + (_spaceBetweenItemsX * (i % _columns)), _yStart + (-_spaceBetweenItemsY * (i / _columns)), 0f);
    }

    void Start()
    {
        CreateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }
}
