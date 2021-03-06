using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [HideInInspector] public SalaManager sala;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        BolsaDeHielo bolsaDeHielo = collision.gameObject.GetComponent<BolsaDeHielo>();
        if (bolsaDeHielo != null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }

    public void Register()
    {
        //Debug.LogWarning("Sala" + sala);
        //Debug.LogWarning("This " + this);
        sala.RegisterDoor(this);
    }
}   