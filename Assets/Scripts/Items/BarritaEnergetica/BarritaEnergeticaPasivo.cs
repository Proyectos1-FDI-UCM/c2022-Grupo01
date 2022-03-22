using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarritaEnergeticaPasivo : PassiveObject
{
    #region methods
    public override void Activate()
    {
        base.Activate();
        PlayerManager.Instance.myLifeState = PlayerManager.LifeStates.Normal;
    }
    #endregion
}
