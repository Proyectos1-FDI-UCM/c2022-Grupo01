using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInventoryPanelManager : MonoBehaviour
{
    [SerializeField] private float _xStart, _yStart;
    [SerializeField] private GameObject _activeInventoryPrefab;
    [HideInInspector] public GameObject activeItemDisplayed;

    public void CreateActiveDisplay()
    {
        if (activeItemDisplayed != null)
        {
            var obj2 = Instantiate(_activeInventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj2.GetComponent<RectTransform>().localPosition = GetActiveSlotPosition();
            activeItemDisplayed = Inventory.Instance.activeItem;
        }
    }

    public void UpdateActiveDisplay()
    {
        if (activeItemDisplayed != Inventory.Instance.activeItem && activeItemDisplayed == null)
        {
            var obj2 = Instantiate(_activeInventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj2.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Inventory.Instance.activeItem.GetComponent<SpriteRenderer>().sprite;

            obj2.GetComponent<RectTransform>().localPosition = GetActiveSlotPosition();

            activeItemDisplayed = Inventory.Instance.activeItem;
        }
        else if (activeItemDisplayed != Inventory.Instance.activeItem && activeItemDisplayed != null)
        {
            activeItemDisplayed = Inventory.Instance.activeItem;
            _activeInventoryPrefab.GetComponentInChildren<Image>().sprite = activeItemDisplayed.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public Vector3 GetActiveSlotPosition()
    {
        return new Vector3(_xStart, _yStart, 0f);
        //return transform.GetChild(0).GetComponentInChildren<RectTransform>().localPosition;
    }

    void Start()
    {
        CreateActiveDisplay();
    }

    void Update()
    {
        UpdateActiveDisplay();
    }
}
