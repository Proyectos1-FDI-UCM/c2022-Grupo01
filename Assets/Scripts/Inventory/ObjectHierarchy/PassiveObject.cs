using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveObject : Object
{
    //Meter atributos aquí
    #region parameters

    public int maxHealth, health, meleeDamage, rangeDamage, speed, manguitos;
    #endregion

    #region properties
    private int[] stats = new int[6];
    #endregion
    private void Start()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            switch (i)
            {
                case 0:
                    stats[i] = maxHealth;
                    break;
                case 1:
                    stats[i] = health;
                    break;
                case 2:
                    stats[i] = meleeDamage;
                    break;
                case 3:
                    stats[i] = rangeDamage;
                    break;
                case 4:
                    stats[i] = speed;
                    break;
                case 5:
                    stats[i] = manguitos;
                    break;
            }
        }
    }

    public virtual void Activate()
    {
        Debug.Log("Objeto pasivo " + gameObject.name + " activado");
        for (int i = 0; i < stats.Length; i++)
        {
            if(stats[i] != 0)
            {
                switch (i)
                {
                    case 0:
                        PlayerManager.Instance.ChangeMaxLife(stats[i]);
                        break;
                    case 1:
                        PlayerManager.Instance.ChangePlayerLife(stats[i], false);
                        break;
                    case 2:
                        PlayerManager.Instance.ChangePlayerMeleeDamage(stats[i]);
                        break;
                    case 3:
                        PlayerManager.Instance.ChangePlayerRangeDamage(stats[i]);
                        break;
                    case 4:
                        PlayerManager.Instance.ChangePlayerSpeed(stats[i]);
                        break;
                    case 5:
                        PlayerManager.Instance.ChangePlayerShields(stats[i]);
                        break;
                }
            }
        }
        PassiveInventoryPanelManager.Instance.UpdatePassiveDisplay();
    }
}
