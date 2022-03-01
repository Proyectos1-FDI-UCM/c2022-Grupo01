using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosteDetector : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        gancho _gancho = collision.gameObject.GetComponent<gancho>();
        if (_gancho != null)
        {
            Debug.Log("Detecta gancho");
            _gancho.LanzaGancho(transform.position);
        }
    }
}