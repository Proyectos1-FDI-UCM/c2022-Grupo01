using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayChestRoomSound : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<AudioManager>().Play("ChestRoom");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //FALTA PONER AQUÍ LA CANCIÓN NORMAL Y CORRIENTE
    }
}
