using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinController : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _interactionRange = 1f;
    [SerializeField] private LayerMask _objectLayer;
    [SerializeField] private GameObject _dummyBottle;
    #endregion

    #region methods
    void InteractWithBin(Collider2D collider)
    {
        Bin bin = collider.GetComponent<Bin>();

        switch(bin.binType)
        {
            case BinType.Glass:
                if(Inventory.Instance.bottleItem.GetComponent<BottleObject>().ID == 1)
                {
                    Debug.Log("Interactuado con una papelera de vidrio");
                    PlayerManager.Instance.ChangePlayerShields(2);
                    Destroy(Inventory.Instance.bottleItem);
                    Inventory.Instance.bottleItem = _dummyBottle;
                }
                else
                {
                    PlayerManager.Instance.ChangePlayerLife(-10f);
                }
                break;

            case BinType.Plastic:
                if(Inventory.Instance.bottleItem.GetComponent<BottleObject>().ID == 2)
                {
                    Debug.Log("Interactuado con una papelera de plástico");
                    PlayerManager.Instance.ChangePlayerLife(20f);
                    Destroy(Inventory.Instance.bottleItem);
                    Inventory.Instance.bottleItem = _dummyBottle;
                }
                else
                {
                    PlayerManager.Instance.ChangePlayerLife(-10f);
                }
                break;

            case BinType.Paper:
                if (Inventory.Instance.bottleItem.GetComponent<BottleObject>().ID == 0)
                {
                    Debug.Log("Interactuado con una papelera de papel");
                    PlayerManager.Instance.ChangeMaxLife(10);
                    Destroy(Inventory.Instance.bottleItem);
                    Inventory.Instance.bottleItem = _dummyBottle;
                }
                else
                {
                    PlayerManager.Instance.ChangePlayerLife(-10f);
                }
                break;    
        }
    }
    #endregion


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Collider2D[] bins = Physics2D.OverlapCircleAll(transform.position, _interactionRange, _objectLayer);
            if (bins.Length >= 1) InteractWithBin(bins[0]);
        }
    }
}
