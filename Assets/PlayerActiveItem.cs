using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActiveItem : MonoBehaviour
{
    [HideInInspector] public int uses;
    [SerializeField] private GameObject fregona;

    public void Activate(int ID)
    {
        switch (ID)
        {
            case 0: //Bolsa de hielo
                GetComponent<PlayerAttack>().LanzaHielo();
                break;

            case 1: //Fregona 
                fregona.SetActive(true);
                break;
            case 2: //Cacharro de Pompas
                break;
            case 3: //Holy Flotador
                break;
        }
    }
}