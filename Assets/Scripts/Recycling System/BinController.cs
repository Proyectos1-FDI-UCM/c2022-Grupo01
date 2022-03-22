using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinController : MonoBehaviour
{
    public enum BinType {Glass, Plastic, Cardboard }
    #region parameters
    [SerializeField] private float _interactionRange = 1f;
    [SerializeField] private LayerMask _objectLayer;
    public BinType _binType;
    #endregion

    #region methods
    void InteractWithBin(Collider2D bin)
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            switch(_binType)
            {
                case BinType.Glass:
                    if(Inventory.Instance.bottleItem.GetComponent<BottleObject>().ID == 1)
                    {
                        PlayerManager.Instance.ChangePlayerShields(2);
                    }
                    else
                    {
                        PlayerManager.Instance.ChangePlayerLife(-10f);
                    }
                    break;

                case BinType.Plastic:
                    if(Inventory.Instance.bottleItem.GetComponent<BottleObject>().ID == 2)
                    {
                        PlayerManager.Instance.ChangePlayerLife(20f);
                    }
                    else
                    {
                        PlayerManager.Instance.ChangePlayerLife(-10f);
                    }
                    break;

                case BinType.Cardboard:
                    if (Inventory.Instance.bottleItem.GetComponent<BottleObject>().ID == 0)
                    {
                        PlayerManager.Instance.ChangeMaxLife(10);
                    }
                    else
                    {
                        PlayerManager.Instance.ChangePlayerLife(-10f);
                    }
                    break;
            }
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Collider2D[] bins = Physics2D.OverlapCircleAll(transform.position, _interactionRange, _objectLayer);
            if (bins.Length >= 1) InteractWithBin(bins[0]);
        }
    }
}
