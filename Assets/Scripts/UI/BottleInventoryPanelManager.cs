using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleInventoryPanelManager : MonoBehaviour
{
    [SerializeField] private float _xStart, _yStart;
    [SerializeField] private GameObject _bottleInventoryPrefab;
    [HideInInspector] public GameObject bottleItemDisplayed;
    private static BottleInventoryPanelManager _instance;
    private GameObject obj2;
    public static BottleInventoryPanelManager Instance
    {
        get { return _instance; }
    }

    private PickUpObjects _pUObjects;

    public void CreateBottleDisplay()
    {
        if (bottleItemDisplayed != null)
        {
            obj2 = Instantiate(_bottleInventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj2.GetComponent<RectTransform>().localPosition = GetBottleSlotPosition();
            bottleItemDisplayed = Inventory.Instance.bottleItem;
        }
    }

    public void UpdateBottleDisplay()
    {
        if (bottleItemDisplayed != Inventory.Instance.bottleItem && bottleItemDisplayed == null)
        {
            obj2 = Instantiate(_bottleInventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj2.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Inventory.Instance.bottleItem.GetComponent<SpriteRenderer>().sprite;

            obj2.GetComponent<RectTransform>().localPosition = GetBottleSlotPosition();

            bottleItemDisplayed = Inventory.Instance.bottleItem;
        }
        else if (bottleItemDisplayed != Inventory.Instance.bottleItem && bottleItemDisplayed != null)
        {
            bottleItemDisplayed = Inventory.Instance.bottleItem;
            obj2.transform.GetChild(0).GetComponentInChildren<Image>().sprite = bottleItemDisplayed.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void RemoveBottleDisplay()
    {
        bottleItemDisplayed = null;
        obj2.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
        Color temp = obj2.transform.GetChild(0).GetComponentInChildren<Image>().color;
        temp.a = 0;
        obj2.transform.GetChild(0).GetComponentInChildren<Image>().color = temp;
        _pUObjects.bottleObjectPickedUp = false;
    }

    public Vector3 GetBottleSlotPosition()
    {
        return new Vector3(_xStart, _yStart, 0f);
        //return transform.GetChild(0).GetComponentInChildren<RectTransform>().localPosition;
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _pUObjects = GetComponent<PickUpObjects>();
        CreateBottleDisplay();
    }
}
