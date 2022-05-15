using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BayetaPasivo : PassiveObject
{
    #region parameters
    private int recuperaVida = 10;
    #endregion

    #region methods
    public override void Activate()
    {
        base.Activate();
        PlayerManager.Instance.bayeta = true;
    }

    public void IncreaseHealth()
    {
        PlayerManager.Instance.ChangePlayerLife(recuperaVida * (PlayerManager.Instance._deadEnemyCount / 10), false);
        PlayerManager.Instance._deadEnemyCount %= 10;
    }
    #endregion
}