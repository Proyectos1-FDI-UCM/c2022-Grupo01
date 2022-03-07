using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveInventoryPanelManager : MonoBehaviour
{
    #region parameters
    [SerializeField] private int _spaceBetweenItemsX, _spaceBetweenItemsY, _columns, _xStart, _yStart;
    [SerializeField] private GameObject _passiveInventoryPrefab;
    List<GameObject> passiveItemsDisplayed = new List<GameObject>();
    private static PassiveInventoryPanelManager _instance;
    public static PassiveInventoryPanelManager Instance
    {
        get { return _instance; }
    }
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

    public void UpdatePassiveDisplay()
    {
        for (int i = 0; i < Inventory.Instance.passiveItemList.Count; i++)
        {
            if (!passiveItemsDisplayed.Contains(Inventory.Instance.passiveItemList[i]))
            {
                var obj = Instantiate(_passiveInventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = Inventory.Instance.passiveItemList[i].GetComponent<SpriteRenderer>().sprite;
                obj.GetComponent<RectTransform>().localPosition = GetPassiveSlotPosition(i);
                passiveItemsDisplayed.Add(Inventory.Instance.passiveItemList[i]);
            }
        }
    }

    public Vector3 GetPassiveSlotPosition(int i)
    {
        return new Vector3(_xStart + (_spaceBetweenItemsX * (i % _columns)), _yStart + (-_spaceBetweenItemsY * (i / _columns)), 0f);
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        CreatePassiveDisplay();
    }

    void Update()
    {
        UpdatePassiveDisplay();
    }
}