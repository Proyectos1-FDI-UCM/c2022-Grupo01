using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FregonaActivo : ActiveObject
{
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
            sonToCreate.GetComponent<FregonaController>().maxUses = maxUses;
            sonCreated = true;
        }
    }

    public override void ChangeActiveObject()
    {
        base.ChangeActiveObject();
        Destroy(sonToCreate);
    }
}