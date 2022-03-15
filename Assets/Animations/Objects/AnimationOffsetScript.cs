using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOffsetScript : MonoBehaviour
{
    /// <summary>
    /// Offset para la animación de los pickable objects
    /// </summary>
    public Vector3 offset;

    void FixedUpdate()
    {
        transform.position += offset; //Cambiamos el transform para la animación
    }
}
