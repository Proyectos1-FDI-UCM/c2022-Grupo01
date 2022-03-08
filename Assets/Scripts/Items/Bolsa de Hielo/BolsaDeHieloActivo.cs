using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaDeHieloActivo : ActiveObject
{
    #region parameters
    [SerializeField] private float _iceForce = 20f;
 	#endregion

	#region references
	[SerializeField] private GameObject iceBagPrefab;
    #endregion

    public override void Activate()
	{
		base.Activate();

        if (!sonCreated)
        {
            base.Activate();
            sonToCreate = new GameObject("BolsaDeHielo");
            sonToCreate.transform.parent = PlayerManager.Instance.player.transform;
            sonToCreate.AddComponent<BolsaDeHieloController>();
            BolsaDeHieloController bolsaDeHieloController = sonToCreate.GetComponent<BolsaDeHieloController>();
            bolsaDeHieloController._icebagPrefab = iceBagPrefab;
            bolsaDeHieloController._cooldown = cooldown;
            bolsaDeHieloController._elapsedTime = cooldown;
            bolsaDeHieloController._iceForce = _iceForce;
            bolsaDeHieloController.maxUses = maxUses;
            sonCreated = true;
        }
    }

	public override void ChangeActiveObject()
    {
        base.ChangeActiveObject();
        Destroy(sonToCreate);
    }
}
