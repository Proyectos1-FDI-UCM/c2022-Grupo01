using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BayetaPasivo : PassiveObject
{

    #region parameters
    private int recuperaVida = 0;
    #endregion

    #region references
    #endregion

    #region methods
    public override void Activate()
    {
        base.Activate();
        PlayerManager.Instance.myLifeState = PlayerManager.LifeStates.Normal;
        int temp = GameManager.Instance._deadEnemyCount;
        recuperaVida = (temp/10) * 10;
        GameManager.Instance._deadEnemyCount = temp % 10;
        PlayerManager.Instance.ChangePlayerLife(recuperaVida);
    }
    #endregion



}
