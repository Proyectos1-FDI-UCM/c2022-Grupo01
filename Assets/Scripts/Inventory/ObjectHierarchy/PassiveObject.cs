using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveObject : Object
{
    //Meter atributos aquí
    #region parameters

    public int health, maxHealth, meleeDamage, rangeDamage, speed;
    #endregion

    #region properties
    private int[] stats = new int[5];
    #endregion
    private void Start()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            switch (i)
            {
                case 0:
                    stats[i] = health;
                    break;
                case 1:
                    stats[i] = maxHealth;
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
                        PlayerManager.Instance.ChangePlayerLife(stats[i]);
                        break;
                    case 1:
                        PlayerManager.Instance.ChangeMaxLife(stats[i]);
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
                }
            }
        }
        PassiveInventoryPanelManager.Instance.UpdatePassiveDisplay();
    }
}
