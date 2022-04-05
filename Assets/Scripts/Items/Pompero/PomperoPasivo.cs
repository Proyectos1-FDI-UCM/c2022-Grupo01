using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PomperoPasivo : PassiveObject
{
    #region properties
    private bool pickedUp = false;
    private GameObject pompero;
    #endregion

    #region references
    [SerializeField] private GameObject _bubblePrefab;
    #endregion

    public override void Activate()
    {
        base.Activate();

        if (!pickedUp)
        {
            base.Activate();
            pompero = new GameObject("CacharroDePompas");
            pompero.transform.parent = PlayerManager.Instance.player.transform;
            pompero.AddComponent<Pomper>();
            pompero.GetComponent<Pomper>()._bubblePrefab = _bubblePrefab;
            pickedUp = true;
        }
    }
}
