using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BebidaEnergeticaActivo : ActiveObject
{
    #region parameters
    [SerializeField] private int speedBoost = 2;
    #endregion

    #region references
    private BebidaEnergeticaController _bebidaEnergéticaController;
    #endregion

    public override void Activate()
    {
        base.Activate();
        if (!sonCreated)
        {
            base.Activate();
            CreateSon();
            sonCreated = true;
        }
    }

    void CreateSon()
    {
        sonToCreate = new GameObject("BebidaEnergética");
        sonToCreate.transform.parent = PlayerManager.Instance.player.transform;
        sonToCreate.AddComponent<BebidaEnergeticaController>();
        _bebidaEnergéticaController = sonToCreate.GetComponent<BebidaEnergeticaController>();
        _bebidaEnergéticaController._coolDown = cooldown;
        _bebidaEnergéticaController.speedBoost = speedBoost;
    }

    public override void ChangeActiveObject()
    {
        base.ChangeActiveObject();
        sonCreated = false;
        Destroy(sonToCreate);
    }
}
