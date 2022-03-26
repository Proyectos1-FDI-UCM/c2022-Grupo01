using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BebidaEnergeticaActivo : ActiveObject
{
    #region parameters
    [SerializeField] private int speedBoost = 2;
    #endregion

    #region references
    private BebidaEnergeticaController _bebidaEnerg�ticaController;
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
        sonToCreate = new GameObject("BebidaEnerg�tica");
        sonToCreate.transform.parent = PlayerManager.Instance.player.transform;
        sonToCreate.AddComponent<BebidaEnergeticaController>();
        _bebidaEnerg�ticaController = sonToCreate.GetComponent<BebidaEnergeticaController>();
        _bebidaEnerg�ticaController._coolDown = cooldown;
        _bebidaEnerg�ticaController.speedBoost = speedBoost;
    }

    public override void ChangeActiveObject()
    {
        base.ChangeActiveObject();
        sonCreated = false;
        Destroy(sonToCreate);
    }
}
