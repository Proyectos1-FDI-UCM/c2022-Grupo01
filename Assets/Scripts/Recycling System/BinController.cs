using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinController : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _interactionRange = 1f;
    [SerializeField] private LayerMask _objectLayer;
    #endregion

    #region methods
    void InteractWithBin(Collider2D bin)
    {

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
