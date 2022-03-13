using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esquinas : MonoBehaviour
{      
    void OnCollisionEnter2D(Collision2D collision)
    {
        BossMovement _boss = collision.gameObject.GetComponent<BossMovement>();

        if (_boss!=null)
        {
            if (_boss.giro != 4)
            {
                _boss.Giro();
            }
            else
            {
                _boss.agua = false;

            }
        }
       
    }

}
