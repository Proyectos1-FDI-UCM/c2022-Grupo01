using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelManager : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _spaceBetweenItemsX, _spaceBetweenItemsY, _columns, _xStart, _yStart, _activeXPos, _activeYPos;
    [SerializeField] private GameObject _passiveInventoryPrefab, _activeInventoryPrefab;
    List<GameObject> passiveItemsDisplayed = new List<GameObject>();
    [HideInInspector]public GameObject activeItemDisplayed;
    #endregion

    public void CreatePassiveDisplay()
    {
        for (int i = 0; i < Inventory.Instance.passiveItemList.Count; i++)
        {
            var obj = Instantiate(_passiveInventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPassiveSlotPosition(i);
            passiveItemsDisplayed.Add(Inventory.Instance.passiveItemList[i]);
        }
    }

    public void CreateActiveDisplay()
    {
        if(activeItemDisplayed != null)
        {
            var obj2 = Instantiate(_activeInventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj2.GetComponent<RectTransform>().localPosition = GetActiveSlotPosition();
            activeItemDisplayed = Inventory.Instance.activeItem;
        }
    }

    public void UpdatePassiveDisplay()
    {
        for(int i = 0; i < Inventory.Instance.passiveItemList.Count; i++)
        {
            if(!passiveItemsDisplayed.Contains(Inventory.Instance.passiveItemList[i]))
            {
                var obj = Instantiate(_passiveInventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Inventory.Instance.passiveItemList[i].GetComponent<SpriteRenderer>().sprite;
                obj.GetComponent<RectTransform>().localPosition = GetPassiveSlotPosition(i);
                passiveItemsDisplayed.Add(Inventory.Instance.passiveItemList[i]);
            }
        }
    }

    public void UpdateActiveDisplay()
    {
        if(activeItemDisplayed != Inventory.Instance.activeItem && activeItemDisplayed == null)
        {
            var obj2 = Instantiate(_activeInventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj2.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Inventory.Instance.activeItem.GetComponent<SpriteRenderer>().sprite;
            obj2.GetComponent<RectTransform>().localPosition = GetActiveSlotPosition();
            activeItemDisplayed = Inventory.Instance.activeItem;
        }
        else if(activeItemDisplayed != Inventory.Instance.activeItem && activeItemDisplayed != null)
        {
            activeItemDisplayed = Inventory.Instance.activeItem;
            _activeInventoryPrefab.GetComponentInChildren<Image>().sprite = activeItemDisplayed.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public Vector3 GetPassiveSlotPosition(int i)
    {
        return new Vector3(_xStart + (_spaceBetweenItemsX * (i % _columns)), _yStart + (-_spaceBetweenItemsY * (i / _columns)), 0f);
    }

    public Vector3 GetActiveSlotPosition()
    {
        return new Vector3(_activeXPos, _activeYPos, 0f);
    }

    void Start()
    {
        CreatePassiveDisplay();
        CreateActiveDisplay();
    }

    void Update()
    {
        UpdatePassiveDisplay();
        UpdateActiveDisplay();
    }
}
