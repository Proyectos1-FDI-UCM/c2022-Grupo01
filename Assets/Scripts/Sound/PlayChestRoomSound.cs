using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayChestRoomSound : MonoBehaviour
{
    #region properties
    private int timesEntered = 0;
    #endregion
    void OnTriggerEnter2D(Collider2D collision)
    {
        timesEntered++;
        if(timesEntered == 1)
        {
            AudioManager.Instance.Play("ChestRoom");
        }   
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        AudioManager.Instance.StopPlaying("ChestRoom");
        timesEntered = 0;
        //FALTA PONER AQUÍ LA CANCIÓN NORMAL Y CORRIENTE
    }
}
