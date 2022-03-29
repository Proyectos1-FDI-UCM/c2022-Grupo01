using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [HideInInspector] public SpongeSalaManager salaSponge;

    public void Register()
    {
        Debug.LogWarning("Sala" + salaSponge);
        Debug.LogWarning("This " + this);
        salaSponge.RegisterDoor(this);
    }
}