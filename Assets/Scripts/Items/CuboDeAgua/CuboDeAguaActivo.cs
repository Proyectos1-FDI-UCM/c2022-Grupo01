using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboDeAguaActivo : ActiveObject
{
    #region parameters
    [SerializeField] private int maxHealthToAdd = 40;
    #endregion

    #region references
    private CuboDeAguaController _cuboDeAguaController;
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
        sonToCreate = new GameObject("CuboDeAgua");
        sonToCreate.transform.parent = PlayerManager.Instance.player.transform;
        sonToCreate.AddComponent<CuboDeAguaController>();
        _cuboDeAguaController = sonToCreate.GetComponent<CuboDeAguaController>();
        _cuboDeAguaController.recuperaVida = maxHealthToAdd;
        _cuboDeAguaController._cooldown = cooldown;
        _cuboDeAguaController._elapsedTime = cooldown;

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
            _cuboDeAguaController.recuperaVida = maxHealthToAdd;
            _cuboDeAguaController._elapsedTime = cooldown;
		}
        catch
		{
            Debug.LogError("No existe _cuboDeAguaController");
		}
    }
}
