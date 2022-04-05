using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    //[HideInInspector]
    public SpongeSalaManager salaSponge;
    //[HideInInspector] 
    public SalaManager salaManager;


    public void Register()
    {
        //Debug.LogWarning("Sala" + salaSponge);
        //Debug.LogWarning("This " + this);
        if(salaSponge != null)
        {
            salaSponge.RegisterDoor(this);
        }
        else if (salaManager != null)
        {
            salaManager.RegisterDoor(this);
        }
    }
}