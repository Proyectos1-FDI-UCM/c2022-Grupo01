using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [HideInInspector]
    public SalaManager sala;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        BolsaDeHielo bolsa = collision.gameObject.GetComponent<BolsaDeHielo>();
        if (bolsa != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sala.GetComponentInParent<SalaManager>();
        sala.RegisterDoor(this);
    }
}
