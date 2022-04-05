using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BebidaEnergeticaActivo : ActiveObject
{
    #region parameters
    [SerializeField] private int speedBoost = 2;
    #endregion

    #region references
    private BebidaEnergeticaController _bebidaEnergeticaController;
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
        _bebidaEnergeticaController = sonToCreate.GetComponent<BebidaEnergeticaController>();
        _bebidaEnergeticaController._cooldown = cooldown;
        _bebidaEnergeticaController.speedBoost = speedBoost;
        _bebidaEnergeticaController._elapsedTime = cooldown;
    }

    public override void ChangeActiveObject()
    {
        base.ChangeActiveObject();
        sonCreated = false;
        Destroy(sonToCreate);
    }

    public override void OnNewFloor()
    {
        try
        {
            _bebidaEnergeticaController._elapsedTime = cooldown;
        }
        catch
        {
            Debug.LogError("No existe _bebidaEnergeticaController");
        }
    }
}
