using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInventoryPanelManager : MonoBehaviour
{
    [SerializeField] private float _xStart, _yStart;
    [SerializeField] private GameObject _activeInventoryPrefab;
    [HideInInspector] public GameObject activeItemDisplayed;
    private static ActiveInventoryPanelManager _instance;
    private GameObject obj2;
    public static ActiveInventoryPanelManager Instance
    {
        get { return _instance; }
    }

    public void UpdateActiveDisplay()
    {
        if (activeItemDisplayed != Inventory.Instance.activeItem && activeItemDisplayed == null)
        {
            obj2 = Instantiate(_activeInventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj2.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Inventory.Instance.activeItem.GetComponent<SpriteRenderer>().sprite;

            obj2.GetComponent<RectTransform>().localPosition = GetActiveSlotPosition();

            activeItemDisplayed = Inventory.Instance.activeItem;

            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (activeItemDisplayed != Inventory.Instance.activeItem && activeItemDisplayed != null)
        {
            activeItemDisplayed = Inventory.Instance.activeItem;
            obj2.transform.GetChild(0).GetComponentInChildren<Image>().sprite = activeItemDisplayed.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public Vector3 GetActiveSlotPosition()
    {
        return new Vector3(_xStart, _yStart, 0f);
        //return transform.GetChild(0).GetComponentInChildren<RectTransform>().localPosition;
    }

    void Awake()
    {
        _instance = this;
    }
}
