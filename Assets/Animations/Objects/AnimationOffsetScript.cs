using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOffsetScript : MonoBehaviour
{
    public Vector3 offset;

    void FixedUpdate()
    {
        transform.position += offset;
    }
}
