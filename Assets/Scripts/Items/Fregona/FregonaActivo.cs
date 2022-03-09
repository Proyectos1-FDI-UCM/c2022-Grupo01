using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FregonaActivo : ActiveObject
{
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
            sonToCreate.GetComponent<FregonaController>()._elapsedTime = cooldown;
            sonToCreate.GetComponent<FregonaController>()._cooldown = cooldown;
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
}