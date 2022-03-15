using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    //[HideInInspector]
    public SpongeSalaManager sala;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        BolsaDeHielo bolsa = collision.gameObject.GetComponent<BolsaDeHielo>();
        if (bolsa != null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }

    public void Register()
    {
        Debug.LogWarning("Sala" + sala);
        Debug.LogWarning("This " + this);
        sala.RegisterDoor(this);
    }
}
