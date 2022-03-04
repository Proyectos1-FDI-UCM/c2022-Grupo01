using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelManager : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _spaceBetweenItemsX, _spaceBetweenItemsY, _columns, _xStart, _yStart;
    [SerializeField] private GameObject inventoryPrefab;
    List<GameObject> itemsDisplayed = new List<GameObject>();
    #endregion

    public void CreateDisplay()
    {
        for (int i = 0; i < InventoryTry2.Instance.passiveItemList.Count; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            itemsDisplayed.Add(InventoryTry2.Instance.passiveItemList[i]);
        }
    }

    public void UpdateDisplay()
    {
        for(int i = 0; i < InventoryTry2.Instance.passiveItemList.Count; i++)
        {
            if(!itemsDisplayed.Contains(InventoryTry2.Instance.passiveItemList[i]))
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = InventoryTry2.Instance.passiveItemList[i].GetComponent<SpriteRenderer>().sprite;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                itemsDisplayed.Add(InventoryTry2.Instance.passiveItemList[i]);
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
