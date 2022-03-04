using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FregonaActivo : ActiveObject
{
    //Atributos necesarios para el funcionamiento del objeto
    #region properties
    private bool fregonaCreada = false;
    #endregion

    public override void Activate()
	{
        if (!fregonaCreada)
        {
		    base.Activate();
            GameObject fregona = new GameObject("Fregona");
            fregona.transform.parent = PlayerManager.Instance.player.transform;
            CircleCollider2D fregonaCollider = fregona.AddComponent<CircleCollider2D>();
            fregonaCollider.radius = 1;
            fregonaCollider.isTrigger = true;
            fregona.AddComponent<Rigidbody2D>().gravityScale = 0;
            fregona.AddComponent<Fregona>();
            fregonaCreada = true;
        }
	}
}
