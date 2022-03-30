using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GetCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<VideoPlayer>().targetCamera = PlayerManager.Instance.cam;   
    }
}
