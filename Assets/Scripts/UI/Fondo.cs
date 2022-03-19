using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo : MonoBehaviour
{
    private float ancho, alto, posXinicial, posYinicial;
    public GameObject cam;
    public float fondoMovil;

    // Start is called before the first frame update
    void Start()
    {
        posXinicial = transform.position.x;
        posYinicial = transform.position.y;
        ancho = GetComponent<SpriteRenderer>().bounds.size.x;
        alto = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float tempX = (cam.transform.position.x * (1 - fondoMovil));
        float tempY = (cam.transform.position.x * (1 - fondoMovil));
        float distX = (cam.transform.position.x * fondoMovil);
        float distY = (cam.transform.position.y * fondoMovil);

        transform.position = new Vector3(posXinicial + distX, posYinicial + distY, transform.position.z);
        if (tempX > posXinicial + ancho)
        {
            posXinicial += ancho;
        }
        else if (tempX < posXinicial - ancho)
        {
            posXinicial -= ancho;
        }
        if (tempY > posYinicial + alto)
        {
            posYinicial += alto;
        }
        else if (tempY < posYinicial - alto)
        {
            posYinicial -= alto;
        }
    }
}