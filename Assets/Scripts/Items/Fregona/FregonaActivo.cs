using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FregonaActivo : ActiveObject
{
    #region properties
    private FregonaController _fregonaController;
	#endregion

	private void Start()
	{

    }
    public override void Activate()
	{
        if (!sonCreated)
        {
		    base.Activate();
            sonToCreate = new GameObject("Fregona");
            sonToCreate.transform.parent = PlayerManager.Instance.player.transform;
            CircleCollider2D fregonaCollider = sonToCreate.AddComponent<CircleCollider2D>();
            fregonaCollider.radius = 1;
            fregonaCollider.isTrigger = true;
            sonToCreate.AddComponent<Rigidbody2D>().gravityScale = 0;
            sonToCreate.AddComponent<FregonaController>();
            _fregonaController = sonToCreate.GetComponent<FregonaController>();
            _fregonaController._elapsedTime = cooldown;
            _fregonaController._cooldown = cooldown;
            sonCreated = true;
            GameManager.Instance.ShowActiveCooldown(_elapsedTime,cooldown);
        }
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
            _fregonaController._elapsedTime = cooldown;
        }
        catch
        {
            Debug.LogError("No existe _fregonaController");
        }
    }
}