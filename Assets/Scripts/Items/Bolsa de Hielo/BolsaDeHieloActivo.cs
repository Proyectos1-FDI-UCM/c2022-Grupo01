using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaDeHieloActivo : ActiveObject
{
    #region properties
    private bool pickedUp = false;
    private GameObject bolsaDeHielo;
    #endregion

    #region references
    [SerializeField] GameObject iceBagPrefab;
    #endregion

    public override void Activate()
	{
		base.Activate();

        if (!pickedUp)
        {
            base.Activate();
            bolsaDeHielo = new GameObject("BolsaDeHielo");
            bolsaDeHielo.transform.parent = PlayerManager.Instance.player.transform;
            bolsaDeHielo.AddComponent<BolsaDeHieloController>();
            bolsaDeHielo.GetComponent<BolsaDeHieloController>()._icebagPrefab = iceBagPrefab;
            bolsaDeHielo.GetComponent<BolsaDeHieloController>().maxUses = maxUses;
            pickedUp = true;
        }
    }

    public override void ChangeActiveObject()
    {
        base.ChangeActiveObject();
        Destroy(bolsaDeHielo);
    }
}
